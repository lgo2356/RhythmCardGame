using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Module;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
    {
        private const string MANAGER_NAME = "[ GameManager ]";

        public BattleSceneData SceneData
        {
            get; private set;
        }

        protected override void Awake()
        {
            base.Awake();

            RemoveDontDestroyOnLoad();
            SetGameObjectName(MANAGER_NAME);

            SceneData = BattleSceneLoader.Instance.GetBattleSceneData();

            Destroy(BattleSceneLoader.Instance.gameObject);
        }

        protected override void Start()
        {
            CharacterManager.Instance.SpawnPlayerCharacter();
            CharacterManager.Instance.SpawnMonsterCharacter();
            RhythmCardManager.Instance.DrawCard(5);
        }
    }
}
