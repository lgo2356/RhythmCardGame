using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class CharacterBuilder
    {
        private GameObject m_Prefab;
        private CharacterFactory m_Factory;
        private Transform m_Parent;
        private Vector2 m_Position;

        public CharacterBuilder SetPrefab(GameObject prefab)
        {
            m_Prefab = prefab;
            return this;
        }

        public CharacterBuilder SetFactory(CharacterFactory factory)
        {
            m_Factory = factory;
            return this;
        }

        public CharacterBuilder SetParent(Transform parent) 
        {
            m_Parent = parent;
            return this;
        }

        public CharacterBuilder SetPosition(Vector2 position)
        {
            m_Position = position;
            return this;
        }

        public Character Build()
        {
            Character character = Instantiate();

            return character;
        }

        private Character Instantiate()
        {
            if (m_Prefab == null)
            {
                throw new ArgumentNullException("Prefab is null.");
            }

            if (m_Factory == null)
            {
                throw new ArgumentNullException("Factory is null.");
            }

            if (m_Parent == null)
            {
                throw new ArgumentNullException("Parent is null.");
            }

            if (m_Position == null)
            {
                throw new ArgumentNullException("Position is null.");
            }

            Character character = m_Factory.GetCharacter(m_Prefab, m_Position, m_Parent);
            return character;
        }
    }

    public class CharacterManager : MonoBehaviour
    {
        private CharacterFactory[] m_CharacterFactories;
        private UI_StatusPanel m_PlayerStatusPanel;
        private UI_StatusPanel m_MonsterStatusPanel;

        public PlayerCharacter Player
        {
            get; private set;
        }

        public MonsterCharacter Monster
        {
            get; private set;
        }

        public void InjectStatusPanel(UI_StatusPanel playerStatusPanel, UI_StatusPanel monsterStatusPanel)
        {
            m_PlayerStatusPanel = playerStatusPanel;
            m_MonsterStatusPanel = monsterStatusPanel;
        }

        public void SpawnPlayerCharacter(CharacterName name, GameObject prefab, Vector2 position, Transform parent)
        {
            switch (name)
            {
                case CharacterName.Slime:
                    {
                        SlimeCharacterFactory factory = gameObject.GetOrAddComponent<SlimeCharacterFactory>();

                        SlimeCharacter slime = new CharacterBuilder()
                            .SetFactory(factory)
                            .SetPrefab(prefab)
                            .SetParent(parent)
                            .SetPosition(position)
                            .Build() as SlimeCharacter;

                        m_PlayerStatusPanel.Connect(slime);
                        
                        Player = slime;
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid character name.");
            }
        }

        public void SpawnMonsterCharacter(CharacterName name, GameObject prefab, Vector2 position, Transform parent)
        {
            switch (name)
            {
                case CharacterName.Cat:
                    {
                        CatCharacterFactory factory = gameObject.GetOrAddComponent<CatCharacterFactory>();

                        CatCharacter cat = new CharacterBuilder()
                            .SetFactory(factory)
                            .SetPrefab(prefab)
                            .SetParent(parent)
                            .SetPosition(position)
                            .Build() as CatCharacter;

                        m_MonsterStatusPanel.Connect(cat);

                        Monster = cat;
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid character name.");
            }
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
