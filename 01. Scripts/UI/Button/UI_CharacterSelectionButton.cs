using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_CharacterSelectionButton : UI_Button
    {
        [SerializeField] private Image m_BackgroundImage;
        [SerializeField] private Image m_CharacterImage;

        private Action<UI_CharacterSelectionButton> m_OnSelectedAction;

        public void SetCharacterSprite(Sprite sprite)
        {
            m_CharacterImage.sprite = sprite;
        }

        public void SetNormalState()
        {
            IsSelected = false;
            m_CharacterImage.color = new Color(1, 1, 1, 0.7f);
        }

        public void SetHighlightedState()
        {
            m_CharacterImage.color = Color.white;
        }

        public void SetSelectedState()
        {
            IsSelected = true;
            m_CharacterImage.color = Color.white;

            m_OnSelectedAction?.Invoke(this);
        }

        public void SetOnSelectedListener(Action<UI_CharacterSelectionButton> callback)
        {
            m_OnSelectedAction -= callback;
            m_OnSelectedAction += callback;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (IsSelected)
            {
                SetNormalState();
            }
            else
            {
                SetSelectedState();
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            SetHighlightedState();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (IsSelected)
            {
                SetSelectedState();
            }
            else
            {
                SetNormalState();
            }
        }

        private void OnDisable()
        {
            m_OnSelectedAction = null;
        }

        private void OnDestroy()
        {
            m_OnSelectedAction = null;
        }
    }
}
