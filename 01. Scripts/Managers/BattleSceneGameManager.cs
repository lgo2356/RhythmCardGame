using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using System.Collections.Generic;
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

        private int m_SelectedCardSequence = -1;
        private UI_BattleScreen m_UIRoot;
        private CharacterManager m_CharacterModule;
        private BattleManager m_BattleModule;

        public UI_BattleScreen UIRoot
        {
            get
            {
                if (m_UIRoot == null)
                {
                    GameObject instance = GameObject.Find("[ UI ]");

                    if (instance != null) 
                    {
                        m_UIRoot = instance.GetOrAddComponent<UI_BattleScreen>();
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

        public BattleSceneData SceneData
        {
            get; private set;
        }

        public RhythmCardData SelectedCard
        { 
            get; set;
        }

        public void NextTurn()
        {             
            BattleModule.NextTurn();
        }

        public void StartGame()
        {
            StartCoroutine(StartGameCoroutine());
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
            yield return new WaitForSeconds(1.0f);

            RhythmCardManager.Instance.DrawCard(5);
            BattleModule.GetFirstTurn();
        }

        private IEnumerator DoBattleCoroutine(int damage)
        { 
            yield return new WaitForSeconds(0.5f);

            CharacterModule.Player.Attack(CharacterModule.Monster, damage);
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
            CharacterModule.SpawnPlayerCharacter(
                SceneData.PlayerCharacterType,
                SceneData.PlayerCharacterPrefab);

            CharacterModule.SpawnMonsterCharacter(
                SceneData.MonsterCharacterType,
                SceneData.MonsterCharacterPrefab);

            StartGame();
        }
    }
}
