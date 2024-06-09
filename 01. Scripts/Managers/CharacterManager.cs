using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        private const string MANAGER_NAME = "[ CharacterManager ]";

        private Factory[] m_CharacterFactories;
        private GameObject m_BattleField;

        public GameObject BattleField
        {
            get
            {
                if (m_BattleField == null)
                {
                    m_BattleField = GameObject.Find("BattleField");
                }

                return m_BattleField;
            }
        }

        public void SpawnPlayerCharacter()
        {
            CharacterName characterType = BattleSceneGameManager.Instance.SceneData.PlayerCharacterType;
            IProduct product = m_CharacterFactories[(int)characterType].GetProduct(Vector2.zero, BattleField.transform);
        }

        public void SpawnMonsterCharacter()
        {
            CharacterName characterType = BattleSceneGameManager.Instance.SceneData.MonsterCharacterType;
            IProduct product = m_CharacterFactories[(int)characterType].GetProduct(Vector2.zero, BattleField.transform);
        }

        private void InitManager()
        {
            RemoveDontDestroyOnLoad();
            SetGameObjectName(MANAGER_NAME);
        }

        private void InitCharacterFactory()
        {
            m_CharacterFactories = new Factory[Enum.GetValues(typeof(CharacterName)).Length];

            m_CharacterFactories[0] = gameObject.GetOrAddComponent<SlimeCharacterFactory>();
            m_CharacterFactories[1] = gameObject.GetOrAddComponent<CatCharacterFactory>();
        }

        protected override void Awake()
        {
            base.Awake();

            InitManager();
            InitCharacterFactory();
        }
    }
}
