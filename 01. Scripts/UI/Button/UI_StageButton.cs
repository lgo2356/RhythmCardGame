using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_StageButton : UI_Button
    {
        [SerializeField] private int m_StageNumber;

        public override void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(transform.localScale * 1.3f, 0.2f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() => 
                {
                    transform.DOScale(transform.localScale / 1.2f, 0.2f)
                        .SetEase(Ease.InOutSine);
                });
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1, 0.2f)
                .SetEase(Ease.InOutSine);
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}
