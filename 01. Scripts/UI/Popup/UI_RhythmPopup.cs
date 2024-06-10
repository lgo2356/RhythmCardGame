using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_RhythmPopup : UI_Popup
    {
        [SerializeField] Transform m_RhythmNoteStartPosTransform;

        public Transform RhythmNoteStartPosTransform
        {
            get
            {
                if (m_RhythmNoteStartPosTransform == null)
                {
                    m_RhythmNoteStartPosTransform = GameObject.Find("RhythmNoteStartPosition").transform;
                }

                return m_RhythmNoteStartPosTransform;
            }
        }

        protected override void OnShow()
        {
            base.OnShow();

            RhythmManager.Instance.StartRhythm();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void Start()
        {
            base.Start();

            Debug.Log("Start");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
        }
    }
}
