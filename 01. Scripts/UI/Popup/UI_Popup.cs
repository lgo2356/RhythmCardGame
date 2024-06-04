using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_Popup : MonoBehaviour
    {
        public Action OnShowAction;
        public Action OnHideAction;

        public void Show()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                OnShow();
            }
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                OnHide();
            }
        }

        public void SetOnShowListener(Action callback)
        {
            OnShowAction = callback;
        }

        public void SetOnHideListener(Action callback)
        {
            OnHideAction = callback;
        }

        protected void OnShow()
        {
            Debug.Log("UI_StageSelectPopup Show");

            OnShowAction?.Invoke();
        }

        protected void OnHide()
        {
            Debug.Log("UI_StageSelectPopup Hide");

            OnHideAction?.Invoke();
        }

        protected virtual void OnDestroy()
        { 
            OnShowAction = null;
            OnHideAction = null;
        }
    }
}
