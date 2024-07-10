using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
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

        public BattleSceneLoader SetPrefsData(string key, string value)
        {
            PlayerPrefs.SetString(key, value);

            return this;
        }

        public BattleSceneLoader SetPrefsData(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);

            return this;
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
                //MonsterCharacterType = (CharacterName)Enum.Parse(
                //    typeof(CharacterName),
                //    PlayerPrefs.GetString(PlayerPrefsKey.MonsterCharacterName, string.Empty)),
            };

            CharacterName playerCharacterName = (CharacterName)Enum.Parse(typeof(CharacterName), PlayerPrefs.GetString(PlayerPrefsKey.PlayerCharacterName, string.Empty));

            switch (playerCharacterName)
            {
                case CharacterName.Slime:
                    {
                        PlayerPrefs.SetString(PlayerPrefsKey.PlayerCharacterConfig, "Assets/05. Data/Character/SlimeCharacterConfig.asset");
                    }
                    break;

                case CharacterName.Pink:
                    {
                        PlayerPrefs.SetString(PlayerPrefsKey.PlayerCharacterConfig, "Assets/05. Data/Character/PinkCharacterConfig.asset");
                    }
                    break;

                case CharacterName.Grape:
                    {
                        PlayerPrefs.SetString(PlayerPrefsKey.PlayerCharacterConfig, "Assets/05. Data/Character/GrapeCharacterConfig.asset");
                    }
                    break;
            }

            BattleSceneDataLoader dataLoader = new();
            dataLoader
                .Add("Assets/04. Prefabs/Character/PlayerCharacter.prefab")
                .Add("Assets/04. Prefabs/Character/MonsterCharacter.prefab")
                .Add("Assets/04. Prefabs/UI_RhythmCard.prefab")
                .Add("Assets/04. Prefabs/RhythmNote.prefab")
                .Add("Assets/04. Prefabs/LongRhythmNote.prefab")
                .Add("Assets/04. Prefabs/RhythmPivot.prefab")
                .OnComplete((type, asset) =>
                {
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

                        case "RhythmNote":
                            m_BattleSceneData.RhythmNotePrefab = asset;
                            break;

                        case "LongRhythmNote":
                            m_BattleSceneData.LongRhythmNotePrefab = asset;
                            break;

                        case "RhythmPivot":
                            m_BattleSceneData.RhythmPivotPrefab = asset;
                            break;
                    }

                    asyncOperation.allowSceneActivation = true;
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
}
