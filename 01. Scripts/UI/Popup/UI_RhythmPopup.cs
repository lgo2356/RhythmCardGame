using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_RhythmPopup : UI_Popup
    {
        [SerializeField] RhythmManager RhythmManager;

        public void StartRhythm(RhythmNoteDto[] datas)
        { 
            RhythmManager.StartRhythm(datas);
        }

        protected override void OnShow()
        {
            base.OnShow();
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
