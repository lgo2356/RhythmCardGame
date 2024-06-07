using DarkChocoSoft.RhythmCardGame.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_RhythmCard : MonoBehaviour, IProduct, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        protected Image m_BackgroundImage;
        protected Image m_FrameImage;
        protected Image m_CardImage;
        protected Tween m_CurTween;
        protected TextMeshProUGUI m_NoteCountText;
        protected RhythmCardConfig m_Config;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (m_CurTween != null && m_CurTween.IsActive() && m_CurTween.IsPlaying())
            {
                m_CurTween.Kill();
            }

            m_CurTween = transform.DOScale(1.2f, 0.2f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    transform.DOScale(transform.localScale / 1.1f, 0.2f)
                        .SetEase(Ease.InOutSine);
                });
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (m_CurTween != null && m_CurTween.IsActive() && m_CurTween.IsPlaying())
            {
                m_CurTween.Kill();
            }

            m_CurTween = transform.DOScale(1, 0.2f)
                .SetEase(Ease.InOutSine);
        }

        public void LoadConfig(string path)
        {
            ResourceManager.Instance.LoadAsync<RhythmCardConfig>(path, (config) =>
            {
                m_Config = config;

                ApplyConfig(config);
            });
        }

        private void ApplyConfig(RhythmCardConfig config)
        {
            m_BackgroundImage.color = config.BackgroundColor;
            m_CardImage.sprite = config.CardSprite;

            if (config.NoteCount == 99)
            {
                m_NoteCountText.text = "Long";
            }
            else
            {
                m_NoteCountText.text = config.NoteCount.ToString();
            }
        }

        protected virtual void Awake()
        {
            m_BackgroundImage = Utils.FindChild<Image>(gameObject, "Background");
            m_FrameImage = Utils.FindChild<Image>(gameObject, "Frame");
            m_CardImage = Utils.FindChild<Image>(gameObject, "Image");
            m_NoteCountText = Utils.FindChild<TextMeshProUGUI>(gameObject, "Text");
        }

        protected virtual void Start()
        {
            
        }
    }
}
