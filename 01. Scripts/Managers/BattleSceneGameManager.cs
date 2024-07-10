using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public struct BattleSceneData
    {
        public int StageNumber;
        public CharacterName PlayerCharacterType;
        public CharacterName MonsterCharacterType;

        public GameObject RhythmCardPrefab;
        public GameObject RhythmNotePrefab;
        public GameObject LongRhythmNotePrefab;
        public GameObject RhythmPivotPrefab;
        public GameObject PlayerCharacterPrefab;
        public GameObject MonsterCharacterPrefab;
    }

    public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
    {
        private const string MANAGER_NAME = "[ GameManager ]";

        private UI_BattleSceneRoot m_UIRoot;
        private CharacterManager m_CharacterModule;
        private BattleManager m_BattleModule;
        private RhythmCardManager m_RhythmCardModule;

        public UI_BattleSceneRoot UIRoot
        {
            get
            {
                if (m_UIRoot == null)
                {
                    GameObject instance = GameObject.Find("[ UI ]");

                    if (instance != null) 
                    {
                        m_UIRoot = instance.GetOrAddComponent<UI_BattleSceneRoot>();
                    }
                    else
                    {
                        throw new System.Exception("UI_BattleScreen가 없습니다.");
                    }
                }

                return m_UIRoot;
            }
        }

        public CharacterManager CharacterModule
        {
            get
            {
                if (m_CharacterModule == null)
                {
                    m_CharacterModule = gameObject.GetOrAddComponent<CharacterManager>();
                    m_CharacterModule.InjectStatusPanel(UIRoot.PlayerStatusPanel, UIRoot.MonsterStatusPanel);
                }

                return m_CharacterModule;
            }
        }

        public BattleManager BattleModule
        {
            get
            {
                if (m_BattleModule == null)
                {
                    m_BattleModule = gameObject.GetOrAddComponent<BattleManager>();
                    m_BattleModule.InjectUI(UIRoot.TurnButton, UIRoot.CardButton);
                    m_BattleModule.InjectCharacter(CharacterModule.Player, CharacterModule.Monster);
                }

                return m_BattleModule;
            }
        }

        public RhythmCardManager RhythmCardModule
        {
            get
            {
                if (m_RhythmCardModule == null)
                {
                    m_RhythmCardModule = gameObject.GetOrAddComponent<RhythmCardManager>();
                }

                return m_RhythmCardModule;
            }
        }

        public BattleSceneData SceneData
        {
            get; private set;
        }

        public void StartGame()
        {
            StartCoroutine(StartGameCoroutine());
        }

        public void UseRhythmCard()
        {
            StartCoroutine(UseRhythmCardCoroutine());
        }

        // rhythmRatio range : 0 ~ 1
        public void DoBattle(float rhythmRatio)
        {
            //TODO: 각종 버프 디버프 적용

            int damage = (int)(CharacterModule.Player.Stat.AttackDamage * rhythmRatio);

            StartCoroutine(DoBattleCoroutine(damage));
        }

        private IEnumerator StartGameCoroutine()
        {
            RhythmCardModule.DrawCard(5);
            BattleModule.GetFirstTurn();

            yield return null;
        }

        private IEnumerator DoBattleCoroutine(int damage)
        { 
            yield return new WaitForSeconds(0.5f);

            CharacterModule.Player.Attack(CharacterModule.Monster, damage);
        }

        private IEnumerator UseRhythmCardCoroutine()
        {
            yield return null;

            RhythmCardModule.UseRhythmCards();

            //TODO : 리듬 전투 페이즈 수정하기
            //PopupManager.Instance.ShowPopup(PopupType.UI_RhythmPopup);
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
            string playerConfigPath = PlayerPrefs.GetString(PlayerPrefsKey.PlayerCharacterConfig, string.Empty);

            CharacterModule.SpawnPlayerCharacter(
                UIRoot.BattleField.PlayerPositionTransform.position,
                UIRoot.BattleField.transform,
                playerConfigPath);

            CharacterModule.SpawnMonsterCharacter(
                UIRoot.BattleField.MonsterPositionTransform.position,
                UIRoot.BattleField.transform);

            StartGame();
        }
    }
}
