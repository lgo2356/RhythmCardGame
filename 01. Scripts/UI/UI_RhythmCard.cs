using DarkChocoSoft.RhythmCardGame.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_RhythmCard : UI_Button
{
    private Tween m_CurTween;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (m_CurTween != null && m_CurTween.IsActive() && m_CurTween.IsPlaying())
        {
            m_CurTween.Kill();
        }

        m_CurTween = transform.DOScale(1.3f, 0.2f)
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

        if (m_CurTween != null && m_CurTween.IsActive() && m_CurTween.IsPlaying())
        {
            m_CurTween.Kill();
        }

        m_CurTween = transform.DOScale(1, 0.2f)
            .SetEase(Ease.InOutSine);
    }
}
