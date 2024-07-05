using DarkChocoSoft.RhythmCardGame.Manager;
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

        protected virtual void ShowBlocker()         
        {
            GameObject blocker = GameObject.Find("UI_Blocker");

            if (blocker == null)
            {
                ResourceManager.Instance.InstantiateAsync("Assets/04. Prefabs/UI/Popup/UI_Blocker.prefab", transform.root, (obj) =>
                {
                    obj.transform.SetSiblingIndex(transform.GetSiblingIndex());
                });
            }
            else
            {
                blocker.SetActive(true);
            }
        }

        protected virtual void HideBlocker()
        {
            GameObject blocker = GameObject.Find("UI_Blocker");

            if (blocker != null)
            {
                blocker.SetActive(false);
            }
        }

        protected virtual void OnShow()
        {
            Debug.Log($"{gameObject.name} Show");

            ShowBlocker();

            OnShowAction?.Invoke();
        }

        protected virtual void OnHide()
        {
            Debug.Log($"{gameObject.name} Hide");

            HideBlocker();

            OnHideAction?.Invoke();
        }

        protected virtual void OnBlockerClick()
        {
            Hide();
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
