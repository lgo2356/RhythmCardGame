using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class Stat : MonoBehaviour
    {
        private int m_MaxHp;
        private int m_CurrnetHp;
        private int m_AttackDamage;
        private int m_Defense;

        public int MaxHp => m_MaxHp;
        public int CurrentHp => m_CurrnetHp;
        public int AttackDamage => m_AttackDamage;
        public int Defense => m_Defense;

        public void Init(int maxHp, int attackDamage)
        {
            m_MaxHp = maxHp;
            m_CurrnetHp = maxHp;
            m_AttackDamage = attackDamage;
        }

        public void SetMaxHp(int value)
        {
            m_MaxHp = value;
        }

        public void SetCurrentHp(int value)
        {
            if (value < 0)
            {
                m_CurrnetHp = 0;
                return;
            }

            if (value > m_MaxHp)
            {
                m_CurrnetHp = m_MaxHp;
                return;
            }

            m_CurrnetHp = value;
        }

        public void SetAttackDamage(int value)
        {
            if (value < 0)
            {
                m_AttackDamage = 0;
                return;
            }

            m_AttackDamage = value;
        }

        public void SetDefense(int value)
        {
            if (value < 0)
            {
                m_Defense = 0;
                return;
            }

            m_Defense = value;
        }
    }
}
