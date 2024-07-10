using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_RhythmPopup : UI_Popup
    {
        [SerializeField] private RectTransform m_tfNoteStartPosition;

        public void InjectNote(RhythmNote note)
        {
            note.transform.SetParent(transform);
            note.transform.position = m_tfNoteStartPosition.position;
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
