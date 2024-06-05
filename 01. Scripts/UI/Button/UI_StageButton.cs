using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.Module;
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
            base.OnPointerClick(eventData);

            StageDataHolder dataHolder = new GameObject()
                .AddComponent<StageDataHolder>();
            StageDataHolderData dataHolderData = new()
            {
                StageNumber = m_StageNumber
            };
            dataHolder.Data = dataHolderData;

            SceneManager.Instance.Load(SceneName.BattleScene);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

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
            base.OnPointerExit(eventData);

            transform.DOScale(1, 0.2f)
                .SetEase(Ease.InOutSine);
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}
