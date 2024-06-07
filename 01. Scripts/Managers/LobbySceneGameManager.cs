using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class LobbySceneGameManager : Singleton<LobbySceneGameManager>
    {
        private const string MANAGER_NAME = "[ LobbySceneGameManager ]";

        public int SelectedStageNumber = -1;
        public CharacterType SelectedCharacterType = CharacterType.Slime;

        public void SaveData()
        {
            PlayerPrefs.SetInt("StageNumber", SelectedStageNumber);
            PlayerPrefs.SetString("CharacterType", SelectedCharacterType.ToString());
            PlayerPrefs.Save();
        }

        public void ReleaseAllReference()
        {
            PopupManager.Instance.UnloadAllPopup();
        }

        protected override void Awake()
        {
            base.Awake();

            SetupName(MANAGER_NAME);
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