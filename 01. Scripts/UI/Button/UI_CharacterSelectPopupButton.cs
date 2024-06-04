using UnityEngine.EventSystems;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_CharacterSelectPopupButton : UI_Button
    {
        public UI_CharacterSelectPopup CharacterSelectPopup;
        public UI_StageSelectPopup StageSelectPopup;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (CharacterSelectPopup.gameObject.activeSelf)
            {
                CharacterSelectPopup.Hide();
            }
            else
            {
                CharacterSelectPopup.Show();
                StageSelectPopup.Hide();
            }
        }

        public override void OnDeselect(BaseEventData eventData)
        {

        }

        private void OnCharacterPopupShow()
        {
            base.OnSelect(null);
        }

        private void OnCharacterPopupHide()
        {
            base.OnDeselect(null);
        }

        protected override void Start()
        {
            base.Start();

            CharacterSelectPopup?.SetOnShowListener(OnCharacterPopupShow);
            CharacterSelectPopup?.SetOnHideListener(OnCharacterPopupHide);
        }
    }
}
