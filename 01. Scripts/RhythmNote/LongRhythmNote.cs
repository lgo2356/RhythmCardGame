using DarkChocoSoft.RhythmCardGame.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class LongRhythmNote : RhythmNote
    {
        [SerializeField] private RectTransform m_FrontNote;
        [SerializeField] private RectTransform m_BackNote;
        [SerializeField] private RectTransform m_Trail;
        [SerializeField] private float m_NoteLength;

        public void SetNoteLength(float length)
        {
            Vector2 sizeDelta = m_Trail.sizeDelta;
            sizeDelta.x = length;
            m_Trail.sizeDelta = sizeDelta;
        }

        //TODO : 총 길이 설정하는 메서드 만들기
    }
}
