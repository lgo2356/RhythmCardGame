using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class CharacterManager : MonoBehaviour
    {
        private CharacterFactory[] m_CharacterFactories;
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
            ICharacter character = m_CharacterFactories[(int)characterType].GetCharacter(Vector2.zero, BattleField.transform);
        }

        public void SpawnMonsterCharacter()
        {
            CharacterName characterType = BattleSceneGameManager.Instance.SceneData.MonsterCharacterType;
            ICharacter character = m_CharacterFactories[(int)characterType].GetCharacter(Vector2.zero, BattleField.transform);
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
