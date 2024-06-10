using DarkChocoSoft.RhythmCardGame.Data;
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
        Image m_BackgroundImage;
        Image m_FrameImage;
        Image m_CardImage;
        Tween m_CurTween;
        TextMeshProUGUI m_NoteCountText;
        RhythmCardConfig m_Config;
        float m_CardScale = 1f;
        bool m_IsSelected = false;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            m_IsSelected = !m_IsSelected;

            if (m_IsSelected)
            {
                Debug.Log(m_Config.CardType);

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

            if (rhythmCardConfig.NoteCount == 99)
            {
                m_NoteCountText.text = "Long";
            }
            else
            {
                m_NoteCountText.text = rhythmCardConfig.NoteCount.ToString();
            }
        }

        public void LoadConfig(string path)
        {
            ResourceManager.Instance.LoadAsync<RhythmCardConfig>(path, (config) =>
            {
                m_Config = config;

                SetConfig(config);
            });
        }

        void OnSelected()
        {
            m_CardScale = 1.1f;

            BattleSceneGameManager.Instance.RhythmCardComboDic
                .Add(BattleSceneGameManager.Instance.SelectedCardSequence, m_Config.CardType);
        }

        void OnDeselected()
        {
            m_CardScale = 1f;
            m_CurTween = transform.DOScale(m_CardScale, 0.2f)
                .SetEase(Ease.InOutSine);

            BattleSceneGameManager.Instance.RhythmCardComboDic
                .Remove(BattleSceneGameManager.Instance.SelectedCardSequence);
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
