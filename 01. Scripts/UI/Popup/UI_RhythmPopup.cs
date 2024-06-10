using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_RhythmPopup : UI_Popup
    {
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
