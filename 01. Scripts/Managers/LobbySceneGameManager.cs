using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Data;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class LobbySceneGameManager : Singleton<LobbySceneGameManager>
    {
        private const string MANAGER_NAME = "[ LobbySceneGameManager ]";

        public CharacterType SelectedCharacterType = CharacterType.Slime;

        public override void Awake()
        {
            base.Awake();

            SetupName(MANAGER_NAME);
        }

        private void Start()
        {
            RemoveDontDestroyOnLoad();
        }
    }
}