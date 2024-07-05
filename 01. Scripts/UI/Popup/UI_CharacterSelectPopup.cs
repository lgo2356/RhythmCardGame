using DarkChocoSoft.Algorithm;
using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_CharacterSelectPopup : UI_Popup
    {
        [SerializeField] private Button m_LeftPageButton;
        [SerializeField] private Button m_RightPageButton;
        [SerializeField] private TextMeshProUGUI m_PageText;
        [SerializeField] private Image m_CharacterImage;
        [SerializeField] private TextMeshProUGUI m_CharacterNameText;

        private CircularList<CharacterSelectPageData> m_PageDatas;
        private CharacterSelectPageData m_CurrentPageData;
        

        public void OnLeftButtonClick()
        { 
            CharacterSelectPageData data = m_PageDatas.Previous();
            
            SetPage(data);
        }

        public void OnRightButtonClick()
        {
            CharacterSelectPageData data = m_PageDatas.Next();

            SetPage(data);
        }

        private void LoadPopupData()
        {
            string path = Application.dataPath + "/01. Scripts/Data/Local/Json/Local_CharacterSelectPageData.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                CharacterSelectPageListData data = JsonUtility.FromJson<CharacterSelectPageListData>(json);
                List<CharacterSelectPageData> pages = data.pages;

                if (pages == null && pages.Count <= 0)
                {
                    throw new System.Exception("UI_CharacterSelectPopup Pages is null");
                }

                CircularList<CharacterSelectPageData> circularList = new();

                foreach (var page in pages)
                {
                    circularList.Add(page);
                }

                InitPage(circularList);

                return;
            }
            else
            {
                Debug.LogError("File not found");
            }
        }

        private void InitPage(CircularList<CharacterSelectPageData> pages)
        {
            m_PageDatas = pages;
            m_CurrentPageData = pages[0];

            SetPage(0);
        }

        private void SetPage(int index)
        {
            CharacterSelectPageData data = m_PageDatas[index];

            SetCharacterImage(data.img_path);
            SetCharacterName(data.name_kr);
            SetPageText(index);

            m_CurrentPageData = data;
        }

        private void SetPage(CharacterSelectPageData data)
        {
            SetCharacterImage(data.img_path);
            SetCharacterName(data.name_kr);
            SetPageText(m_PageDatas.IndexOf(data));

            m_CurrentPageData = data;
        }

        private void SetCharacterImage(string path)
        {
            ResourceManager.Instance.LoadAsync<Sprite>(path, (sprite) =>
            {
                m_CharacterImage.sprite = sprite;
            });
        }

        private void SetCharacterName(string name)
        {
            m_CharacterNameText.text = name;
        }

        private void SetPageText(int index)
        {
            m_PageText.text = $"{index + 1}/{m_PageDatas.Count}";
        }

        protected override void Awake()
        {
            base.Awake();

            LoadPopupData();
        }
    }
}
