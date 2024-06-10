using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    /**
     * 클래스 이름과 동일하게 해야한다.
     */
    public enum PopupType
    {
        UI_CharacterSelectPopup, UI_RhythmPopup,
    }

    public class PopupManager : Singleton<PopupManager>
    {
        private const string MANAGER_NAME = "[ PopupManager ]";

        public Dictionary<PopupType, UI_Popup> LoadedPopups = new();
        
        private Dictionary<PopupType, UI_Popup> m_ShowingPopupsDic = new();

        public void LoadPopup(string path, Transform parent = null)
        {
            ResourceManager.Instance.InstantiateAsync(path, parent, (obj) =>
            {
                if (obj.TryGetComponent<UI_Popup>(out var uiPopup))
                {
                    if (Enum.TryParse<PopupType>(uiPopup.GetType().Name, out var popupType))
                    {
                        LoadedPopups.Add(popupType, uiPopup);
                    }
                }
                else
                {
                    throw new System.Exception("UI_Popup 컴포넌트가 없습니다.");
                }
            });
        }

        public void LoadPopup(AssetReference assetRef, Transform parent = null)
        {
            ResourceManager.Instance.InstantiateAsync(assetRef, parent, (obj) =>
            {
                if (obj.TryGetComponent<UI_Popup>(out var uiPopup))
                {
                    if (Enum.TryParse<PopupType>(uiPopup.GetType().Name, out var popupType))
                    {
                        LoadedPopups.Add(popupType, uiPopup);
                    }
                }
                else
                {
                    throw new System.Exception("UI_Popup 컴포넌트가 없습니다.");
                }
            });
        }

        public void UnloadPopup(PopupType popupType)
        {
            if (LoadedPopups.ContainsKey(popupType))
            {
                ResourceManager.Instance.ReleaseGameObject(LoadedPopups[popupType].gameObject);

                LoadedPopups.Remove(popupType);
            }
        }

        public void UnloadAllPopup()
        {
            foreach (var popupType in LoadedPopups)
            {
                ResourceManager.Instance.ReleaseGameObject(popupType.Value.gameObject);
            }

            LoadedPopups.Clear();
        }

        public void ShowPopup(PopupType popupType)
        {
            UI_Popup uiPopup = LoadedPopups[popupType];

            if (uiPopup == null)
            {
                throw new System.Exception("로드된 팝업이 없습니다.");
            }

            uiPopup.Show();

            m_ShowingPopupsDic.Add(popupType, uiPopup);
        }

        public void HidePopup(PopupType popup)
        {
            UI_Popup uiPopup = m_ShowingPopupsDic[popup];

            if (uiPopup == null)
            {
                throw new System.Exception("띄워진 팝업이 없습니다.");
            }

            uiPopup.Hide();
            
            m_ShowingPopupsDic.Remove(popup);
        }

        public void HideAllPopup()
        {
            foreach (var popup in m_ShowingPopupsDic)
            {
                popup.Value.Hide();
            }

            m_ShowingPopupsDic.Clear();
        }

        public UI_Popup GetPopup<T>() where T : UI_Popup
        {
            PopupType popupType = (PopupType)Enum.Parse(typeof(PopupType), typeof(T).Name);

            if (m_ShowingPopupsDic.ContainsKey(popupType))
            {
                return m_ShowingPopupsDic[popupType];
            }

            return null;
        }

        public bool IsShowing(PopupType popup)
        {
            return m_ShowingPopupsDic.ContainsKey(popup);
        }

        protected override void Awake()
        {
            base.Awake();

            SetGameObjectName(MANAGER_NAME);
        }

        private void OnDestroy()
        {
            
        }
    }
}
