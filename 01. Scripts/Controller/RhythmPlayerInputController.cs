using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmPlayerInputController : PlayerInputController
    {
        RhythmNoteHitTimingManager m_TimingManager;

        public void OnHitRhythmNote(InputAction.CallbackContext context)
        {
            Debug.Log("OnHitRhythmNote");

            m_TimingManager.CheckHitTiming();
        }

        private void OnReleaseRhythmNote(InputAction.CallbackContext context)
        {
            Debug.Log("OnReleaseRhythmNote");

            m_TimingManager.CheckReleaseTiming();
        }

        protected override void Awake()
        {
            base.Awake();

            m_TimingManager = FindObjectOfType<RhythmNoteHitTimingManager>();
        }

        protected override void Start()
        {
            base.Start();

            PlayerInput playerInput = GetComponent<PlayerInput>();
            playerInput.actions["HitRhythmNote"].started += OnHitRhythmNote;
            playerInput.actions["HitRhythmNote"].canceled += OnReleaseRhythmNote;
        }
    }
}
