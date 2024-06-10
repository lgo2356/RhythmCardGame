using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
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
        public GameObject RhythmPivotPrefab;
        public GameObject PlayerCharacterPrefab;
        public GameObject MonsterCharacterPrefab;
    }

    public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
    {
        private const string MANAGER_NAME = "[ GameManager ]";

        private int m_SelectedCardSequence = -1;

        public BattleSceneData SceneData
        {
            get; private set;
        }

        public Dictionary<int, RhythmCardType> RhythmCardComboDic
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
