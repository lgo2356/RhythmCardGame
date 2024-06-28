using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class CharacterManager : MonoBehaviour
    {
        private CharacterFactory[] m_CharacterFactories;
        private BattleField m_BattleField;

        public BattleField BattleField
        {
            get
            {
                if (m_BattleField == null)
                {
                    GameObject instance = GameObject.Find("BattleField");
                    m_BattleField = instance.GetOrAddComponent<BattleField>();
                }

                return m_BattleField;
            }
        }

        public PlayerCharacter Player
        {
            get; private set;
        }

        public MonsterCharacter Monster
        {
            get; private set;
        }

        public void SpawnPlayerCharacter(CharacterName name, GameObject prefab)
        {
            switch (name)
            {
                case CharacterName.Slime:
                    {
                        SlimeCharacterFactory factory = gameObject.GetOrAddComponent<SlimeCharacterFactory>();
                        Vector2 position = BattleField.PlayerPositionTransform.position;
                        SlimeCharacter slime = factory.GetCharacter(prefab, position, BattleField.transform) as SlimeCharacter;

                        Player = slime;
                    }
                    break;
            }
        }

        public void SpawnMonsterCharacter(CharacterName name, GameObject prefab)
        {
            switch (name)
            {
                case CharacterName.Cat:
                    {
                        CatCharacterFactory factory = gameObject.GetOrAddComponent<CatCharacterFactory>();
                        Vector2 position = BattleField.MonsterPositionTransform.position;
                        CatCharacter cat = factory.GetCharacter(prefab, position, BattleField.transform) as CatCharacter;

                        Monster = cat;
                    }
                    break;
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
