using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
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
                StageNumber = PlayerPrefs.GetInt(PlayerPrefsKey.StageNumber, -1),
                PlayerCharacterType = (CharacterName)Enum.Parse(
                    typeof(CharacterName), 
                    PlayerPrefs.GetString(PlayerPrefsKey.PlayerCharacterName, string.Empty)),
                MonsterCharacterType = (CharacterName)Enum.Parse(
                    typeof(CharacterName),
                    PlayerPrefs.GetString(PlayerPrefsKey.MonsterCharacterName, string.Empty)),
            };

            Debug.Log($"Stage Number: {m_BattleSceneData.StageNumber}");
            Debug.Log($"Selected Character: {m_BattleSceneData.PlayerCharacterType}");

            BattleSceneDataLoader dataLoader = new();
            dataLoader
                .Add("Assets/04. Prefabs/Character/PlayerCharacter.prefab")
                .Add("Assets/04. Prefabs/Character/MonsterCharacter.prefab")
                .Add("Assets/04. Prefabs/UI_RhythmCard.prefab")
                .OnComplete((type, asset) =>
                {
                    Debug.Log($"Asset type : {type}");

                    switch (type)
                    {
                        case "PlayerCharacter":
                            m_BattleSceneData.PlayerCharacterPrefab = asset;
                            break;

                        case "MonsterCharacter":
                            m_BattleSceneData.MonsterCharacterPrefab = asset;
                            break;

                        case "UI_RhythmCard":
                            m_BattleSceneData.RhythmCardPrefab = asset;
                            break;
                    }

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
        public CharacterName PlayerCharacterType;
        public CharacterName MonsterCharacterType;

        public GameObject RhythmCardPrefab;
        public GameObject PlayerCharacterPrefab;
        public GameObject MonsterCharacterPrefab;
    }
}
