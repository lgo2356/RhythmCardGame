using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class LobbySceneGameManager : Singleton<LobbySceneGameManager>
    {
        private const string MANAGER_NAME = "[ LobbySceneGameManager ]";

        public int SelectedStageNumber { get; set; } = -1;
        public CharacterName SelectedCharacterType { get; set; } = CharacterName.Slime;

        public void SaveData()
        {
            //PlayerPrefs.SetInt(PlayerPrefsKey.StageNumber, SelectedStageNumber);
            PlayerPrefs.SetString(PlayerPrefsKey.PlayerCharacterName, SelectedCharacterType.ToString());
            //PlayerPrefs.SetString(PlayerPrefsKey.MonsterCharacterName, CharacterName.Cat.ToString());
            PlayerPrefs.Save();
        }

        public void ReleaseAllReference()
        {
            PopupManager.Instance.UnloadAllPopup();
        }

        protected override void Awake()
        {
            base.Awake();

            SetGameObjectName(MANAGER_NAME);
        }

        protected override void Start()
        {
            RemoveDontDestroyOnLoad();
        }

        private void OnDestroy()
        {
            ReleaseAllReference();
        }
    }
}