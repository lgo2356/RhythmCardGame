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

        protected virtual void OnShow()
        {
            Debug.Log($"{gameObject.name} Show");

            OnShowAction?.Invoke();
        }

        protected virtual void OnHide()
        {
            Debug.Log($"{gameObject.name} Hide");

            OnHideAction?.Invoke();
        }

        protected virtual void Awake()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        { 
            OnShowAction = null;
            OnHideAction = null;
        }
    }
}
