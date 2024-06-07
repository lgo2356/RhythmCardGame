using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
    {
        private const string MANAGER_NAME = "[ BattleSceneGameManager ]";

        public int StageNumber;
        public CharacterType SelectedCharacterType;

        protected override void Awake()
        {
            base.Awake();

            SetGameObjectName(MANAGER_NAME);
        }

        protected override void Start()
        {
            RemoveDontDestroyOnLoad();
        }
    }
}
