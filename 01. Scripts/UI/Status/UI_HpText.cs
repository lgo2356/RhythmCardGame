using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_HpText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_MaxHpText;
        [SerializeField] private TextMeshProUGUI m_CurrentHpText;

        public void InitHp(int value)
        {
            m_MaxHpText.text = value.ToString();
            m_CurrentHpText.text = value.ToString();
        }

        public void SetMaxHp(int value)
        {
            m_MaxHpText.text = value.ToString();
        }

        public void SetCurrentHp(int value)
        {
            m_CurrentHpText.text = value.ToString();
        }
    }
}
