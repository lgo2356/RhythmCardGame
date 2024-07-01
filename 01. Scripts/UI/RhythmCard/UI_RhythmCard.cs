using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    /**
     * 리듬 난이도에 맞춰 비트를 선택한다.
     */
    public class UI_RhythmCard : MonoBehaviour, IRhythmCard, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        Image m_BackgroundImage;
        Image m_FrameImage;
        Image m_CardImage;
        Tween m_CurTween;
        TextMeshProUGUI m_NoteCountText;
        RhythmCardConfig m_Config;
        RhythmCardData m_Data;
        float m_CardScale = 1f;
        bool m_IsSelected = false;
        Action<UI_RhythmCard> m_OnSelectedAction;
        Action<UI_RhythmCard> m_OnDeselectedAction;
        Action<UI_RhythmCard> m_OnUseAction;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            m_IsSelected = !m_IsSelected;

            if (m_IsSelected)
            {
                BattleSceneGameManager.Instance.RhythmCardModule.DeselectAllCardExcept(this);

                OnSelected();
            }
            else 
            { 
                OnDeselected();
            }
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

            m_CurTween = transform.DOScale(m_CardScale, 0.2f)
                .SetEase(Ease.InOutSine);
        }

        public void SetConfig(ScriptableObject config)
        {
            RhythmCardConfig rhythmCardConfig = config as RhythmCardConfig;
            m_BackgroundImage.color = rhythmCardConfig.BackgroundColor;
            m_CardImage.sprite = rhythmCardConfig.CardSprite;
            m_NoteCountText.text = "";
        }

        public void LoadConfig(string path)
        {
            ResourceManager.Instance.LoadAsync<RhythmCardConfig>(path, (config) =>
            {
                m_Config = config;

                SetConfig(config);
            });
        }

        public void Use()
        {
            m_OnUseAction?.Invoke(this);

            Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void SetOnSelectedListener(Action<UI_RhythmCard> callback)
        {
            m_OnSelectedAction -= callback;
            m_OnSelectedAction += callback;
        }

        public void SetOnDeselectedListener(Action<UI_RhythmCard> callback)
        {
            m_OnDeselectedAction -= callback;
            m_OnDeselectedAction += callback;
        }

        public void SetOnUseListener(Action<UI_RhythmCard> callback)
        {
            m_OnUseAction -= callback;
            m_OnUseAction += callback;
        }

        public void OnSelected()
        {
            m_IsSelected = true;
            m_CardScale = 1.1f;

            m_OnSelectedAction?.Invoke(this);
        }

        public void OnDeselected()
        {
            m_IsSelected = false;
            m_CardScale = 1f;

            if (m_CurTween != null && m_CurTween.IsActive() && m_CurTween.IsPlaying())
            {
                m_CurTween.Kill();
            }

            m_CurTween = transform.DOScale(m_CardScale, 0.2f)
                .SetEase(Ease.InOutSine);

            m_OnDeselectedAction?.Invoke(this);
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

        private void OnDestroy()
        {
            m_OnSelectedAction = null;
            m_OnDeselectedAction = null;
            m_OnUseAction = null;
            m_CurTween = null;
        }
    }
}
