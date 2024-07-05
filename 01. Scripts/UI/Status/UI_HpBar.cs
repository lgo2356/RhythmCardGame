using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_HpBar : MonoBehaviour
    {
        [SerializeField] private Slider m_HpSlider;
        [SerializeField] private float m_HpReduceSpeed = 0.3f;

        private int m_CurrentHp;
        private Tween m_CurTween;

        public void InitHp(int value)
        {
            m_HpSlider.maxValue = value;
            m_HpSlider.value = value;
        }

        public void SetMaxHp(int value)
        {
            m_HpSlider.maxValue = value;
        }

        public void SetCurrentHp(int value)
        {
            if (m_CurTween != null && m_CurTween.IsActive() && m_CurTween.IsPlaying())
            {
                m_CurTween.Kill();
            }

            m_CurTween = DOTween.Sequence()
                .Append(m_HpSlider.DOValue(value, m_HpReduceSpeed))
                .SetEase(Ease.OutExpo)
                .OnKill(() =>
                {
                    m_HpSlider.value = value;
                })
                .Play();
        }
    }
}
