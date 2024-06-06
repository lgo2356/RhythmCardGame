using DarkChocoSoft.RhythmCardGame.Manager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_RhythmCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected AssetReference ConfigAssetRef;

        protected Image m_BackgroundImage;
        protected Image m_FrameImage;
        protected Image m_CardImage;
        protected Tween m_CurTween;
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

        private void LoadConfig()
        {
            ResourceManager.Instance.LoadAsset<RhythmCardConfig>(ConfigAssetRef, (config) =>
            {
                m_Config = config;

                ApplyConfig(config);
            });
        }

        private void ApplyConfig(RhythmCardConfig config)
        {
            m_BackgroundImage.color = config.BackgroundColor;
            m_CardImage.sprite = config.CardSprite;
        }

        protected virtual void Awake()
        {
            m_BackgroundImage = Utils.FindChild<Image>(gameObject, "Background");
            m_FrameImage = Utils.FindChild<Image>(gameObject, "Frame");
            m_CardImage = Utils.FindChild<Image>(gameObject, "Image");
        }

        protected virtual void Start()
        {
            LoadConfig();
        }
    }
}
