using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_StatusPanel : MonoBehaviour
    {
        [SerializeField] private UI_HpBar m_HpBar;
        [SerializeField] private UI_HpText m_HpText;

        private Character m_ConnectedCharacter;

        public void Connect(Character character)
        {
            m_ConnectedCharacter = character;
            m_ConnectedCharacter.ConnectStatusPanel(this);
        }

        public void InitHp(int value)
        {
            m_HpText.InitHp(value);
            m_HpBar.InitHp(value);
        }

        public void SetMaxHp(int value)
        {
            m_HpText.SetMaxHp(value);
            m_HpBar.SetMaxHp(value);
        }

        public void SetCurrentHp(int value)
        {
            m_HpText.SetCurrentHp(value);
            m_HpBar.SetCurrentHp(value);
        }

        public void Refresh()
        {
            SetMaxHp(m_ConnectedCharacter.Stat.MaxHp);
            SetCurrentHp(m_ConnectedCharacter.Stat.CurrentHp);
        }
    }
}
