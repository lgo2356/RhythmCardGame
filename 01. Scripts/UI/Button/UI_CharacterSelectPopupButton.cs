using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_CharacterSelectPopupButton : UI_Button
    {
        public UI_CharacterSelectPopup CharacterSelectPopup;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (PopupManager.Instance.IsShowing(PopupType.UI_CharacterSelectPopup))
            {
                PopupManager.Instance.HidePopup(PopupType.UI_CharacterSelectPopup);
                IsSelected = false;
            }
            else
            {
                PopupManager.Instance.ShowPopup(PopupType.UI_CharacterSelectPopup);
                IsSelected = true;
            }

            m_Image.sprite = IsSelected ? SelectedSprite : NormalSprite;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            m_Image.sprite = HighlightedSprite;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            m_Image.sprite = IsSelected ? SelectedSprite : NormalSprite;
        }

        private void OnCharacterPopupShow()
        {
            IsSelected = true;
            m_Image.sprite = HighlightedSprite;
        }

        private void OnCharacterPopupHide()
        {
            IsSelected = false;
            m_Image.sprite = NormalSprite;
        }

        protected override void Start()
        {
            base.Start();

            CharacterSelectPopup?.SetOnShowListener(OnCharacterPopupShow);
            CharacterSelectPopup?.SetOnHideListener(OnCharacterPopupHide);
        }
    }
}
