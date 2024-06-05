using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_CharacterSelectPopupButton : UI_Button
    {
        public UI_CharacterSelectPopup CharacterSelectPopup;
        public Sprite NormalSprite;
        public Sprite HighlightedSprite;
        public Sprite SelectedSprite;
        public bool IsSelected = false;

        private Image m_Image;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (CharacterSelectPopup.gameObject.activeSelf)
            {
                CharacterSelectPopup.Hide();
                IsSelected = false;
            }
            else
            {
                CharacterSelectPopup.Show();
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

        private void Awake()
        {
            m_Image = GetComponent<Image>();
        }

        private void Start()
        {
            CharacterSelectPopup?.SetOnShowListener(OnCharacterPopupShow);
            CharacterSelectPopup?.SetOnHideListener(OnCharacterPopupHide);
        }
    }
}
