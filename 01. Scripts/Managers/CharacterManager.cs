using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlayerPrefab;
        [SerializeField] private GameObject m_MonsterPrefab;

        private CharacterFactory[] m_CharacterFactories;
        private UI_StatusPanel m_PlayerStatusPanel;
        private UI_StatusPanel m_MonsterStatusPanel;

        public Character Player
        {
            get; private set;
        }

        public Character Monster
        {
            get; private set;
        }

        public void InjectStatusPanel(UI_StatusPanel playerStatusPanel, UI_StatusPanel monsterStatusPanel)
        {
            m_PlayerStatusPanel = playerStatusPanel;
            m_MonsterStatusPanel = monsterStatusPanel;
        }

        public void SpawnPlayerCharacter(Vector2 position, Transform parent, string configPath)
        {
            Character character = new CharacterBuilder()
                .SetPrefab(m_PlayerPrefab)
                .SetParent(parent)
                .SetPosition(position)
                .SetConfig(configPath)
                .SetStatusPanel(m_PlayerStatusPanel)
                .Build();

            Player = character;
        }

        public void SpawnMonsterCharacter(Vector2 position, Transform parent)
        {
            string configPath = "Assets/05. Data/Character/CatCharacterConfig.asset";

            Character character = new CharacterBuilder()
                .SetPrefab(m_MonsterPrefab)
                .SetParent(parent)
                .SetPosition(position)
                .SetConfig(configPath)
                .SetStatusPanel(m_MonsterStatusPanel)
                .Build();

            Monster = character;
        }

        private void InitCharacterFactory()
        {
            m_CharacterFactories = new CharacterFactory[Enum.GetValues(typeof(CharacterName)).Length];

            m_CharacterFactories[0] = gameObject.GetOrAddComponent<SlimeCharacterFactory>();
            m_CharacterFactories[1] = gameObject.GetOrAddComponent<CatCharacterFactory>();
        }

        void Awake()
        {
            InitCharacterFactory();
        }
    }
}
