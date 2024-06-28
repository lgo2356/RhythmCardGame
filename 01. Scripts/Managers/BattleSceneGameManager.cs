using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
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
        private CharacterManager m_CharacterModule;
        private BattleManager m_BattleModule;

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

        public Dictionary<int, RhythmCardData> RhythmCardComboDic
        {
            get; set;
        } = new();

        public int SelectedCardSequence
        {
            get
            {
                m_SelectedCardSequence++;

                return m_SelectedCardSequence;
            }

            set
            { 
                m_SelectedCardSequence = value; 
            }
        }

        public void DoBattle(float rhythmRatio)
        {
            //TODO: 각종 버프 디버프 적용
            //TODO: 리듬 성공률에 따라 대미지 계산

            CharacterModule.Player.Attack(CharacterModule.Monster, 10f);
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
            
            RhythmCardManager.Instance.DrawCard(5);
        }
    }
}
