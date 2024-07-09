using DarkChocoSoft.Algorithm;
using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_CharacterSelectionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject m_CharacterSelectionButtonPrefab;

        private void LoadCharacterButtonData()
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

                Init(circularList);

                return;
            }
            else
            {
                Debug.LogError("File not found");
            }
        }

        private void Init(CircularList<CharacterSelectPageData> datas)
        {
            InitCharacterSelectionButtons(datas);
        }

        private void SetButtonNormalStateExcept(UI_CharacterSelectionButton exceptButton)
        {
            foreach (var button in GetComponentsInChildren<UI_CharacterSelectionButton>())
            {
                if (button != exceptButton)
                {
                    button.SetNormalState();
                }
            }
        }

        private void InitCharacterSelectionButtons(CircularList<CharacterSelectPageData> datas)
        {
            foreach (var data in datas)
            {
                GameObject instance = ResourceManager.Instance.Instantiate(m_CharacterSelectionButtonPrefab, transform);

                if (instance.TryGetComponent<UI_CharacterSelectionButton>(out var button))
                {
                    ResourceManager.Instance.LoadAsync<Sprite>(data.img_path, (sprite) =>
                    {
                        button.SetCharacterSprite(sprite);
                    });

                    button.SetNormalState();
                    button.SetOnSelectedListener(SetButtonNormalStateExcept);
                }
                else
                {
                    throw new System.Exception("UI_CharacterSelectionButton is null");
                }
            }
        }

        private void Awake()
        {
            LoadCharacterButtonData();
        }
    }
}
