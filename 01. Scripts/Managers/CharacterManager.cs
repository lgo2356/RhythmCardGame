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

        private GameObject m_BattleField;
        private Factory[] m_CharacterFactories;

        public void SpawnPlayerCharacter()
        {
            CharacterName characterType = BattleSceneGameManager.Instance.SceneData.PlayerCharacterType;
            IProduct product = m_CharacterFactories[(int)characterType].GetProduct(Vector2.zero, m_BattleField.transform);
        }

        public void SpawnMonsterCharacter()
        {
            CharacterName characterType = BattleSceneGameManager.Instance.SceneData.MonsterCharacterType;
            IProduct product = m_CharacterFactories[(int)characterType].GetProduct(Vector2.zero, m_BattleField.transform);
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

            m_BattleField = GameObject.Find("BattleField");
        }
    }
}
