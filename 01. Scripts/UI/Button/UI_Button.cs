using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Sprite NormalSprite;
        public Sprite HighlightedSprite;
        public Sprite SelectedSprite;
        public bool IsSelected = false;

        protected Image m_Image;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter");
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
        }

        protected virtual void Awake()
        {
            m_Image = GetComponent<Image>();
        }

        protected virtual void Start()
        {
            if (NormalSprite == null)
            {
                NormalSprite = m_Image.sprite;
            }
        }
    }
}
