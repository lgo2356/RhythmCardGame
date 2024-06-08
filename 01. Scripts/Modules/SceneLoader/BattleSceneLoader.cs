using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class BattleSceneLoader : Singleton<BattleSceneLoader>, ISceneLoader
    {
        private const string MANAGER_NAME = "[ BattleSceneLoader ]";

        private BattleSceneData m_BattleSceneData;

        public void Load(SceneName sceneName)
        {
            string name = SceneName.BattleScene.ToString();

            Addressables.InstantiateAsync(AssetPath.LOADING_SCREEN_PATH).Completed += (obj) =>
            {
                UI_LoadingScreen loadingScreen = obj.Result.GetComponent<UI_LoadingScreen>();
                loadingScreen.SetLoadingImage();

                StartCoroutine(LoadSceneAsyncCoroutine(name));
            };
        }

        public IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            m_BattleSceneData = new BattleSceneData()
            {
                StageNumber = PlayerPrefs.GetInt("StageNumber", -1),
                SelectedCharacterType = PlayerPrefs.GetString("CharacterType", string.Empty)
            };

            Debug.Log($"Stage Number: {m_BattleSceneData.StageNumber}");
            Debug.Log($"Selected Character: {m_BattleSceneData.SelectedCharacterType}");

            BattleSceneDataLoader dataLoader = new();
            dataLoader
                .Add("Assets/04. Prefabs/UI_RhythmCard.prefab")
                .OnComplete((asset) =>
                {
                    m_BattleSceneData.RhythmCardPrefab = asset;

                    asyncOperation.allowSceneActivation = true;

                    Debug.Log($"Task1 complete");
                })
                .Load();

            yield return null;
        }

        public BattleSceneData GetBattleSceneData()
        {
            return m_BattleSceneData;
        }

        protected override void Awake()
        {
            base.Awake();

            SetGameObjectName(MANAGER_NAME);
        }
    }

    public struct BattleSceneData
    {
        public int StageNumber;
        public string SelectedCharacterType;
        public GameObject RhythmCardPrefab;
    }
}
