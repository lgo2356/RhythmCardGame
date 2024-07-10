using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class CharacterBuilder
    {
        private GameObject m_Prefab;
        private CharacterFactory m_Factory;
        private Transform m_Parent;
        private Vector2 m_Position;
        private string m_ConfigPath;
        private UI_StatusPanel m_StatusPanel;

        public CharacterBuilder SetPrefab(GameObject prefab)
        {
            m_Prefab = prefab;
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

        public CharacterBuilder SetConfig(string path)
        {
            m_ConfigPath = path;
            return this;
        }

        public CharacterBuilder SetStatusPanel(UI_StatusPanel statusPanel)
        {
            m_StatusPanel = statusPanel;
            return this;
        }

        public Character Build()
        {
            if (m_Prefab == null)
            {
                throw new ArgumentNullException("Prefab is null.");
            }

            if (m_Parent == null)
            {
                throw new ArgumentNullException("Parent is null.");
            }

            if (m_Position == null)
            {
                throw new ArgumentNullException("Position is null.");
            }

            Character character = ResourceManager.Instance.Instantiate(m_Prefab, m_Parent)
                .GetOrAddComponent<Character>();
            character.transform.position = m_Position;

            ResourceManager.Instance.LoadAsync<CharacterConfig>(m_ConfigPath, (config) =>
            {
                character.SetConfig(config);
                m_StatusPanel.Connect(character);
            });

            return character;
        }
    }
}
