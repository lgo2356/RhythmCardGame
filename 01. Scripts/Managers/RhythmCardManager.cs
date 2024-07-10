using DarkChocoSoft.Algorithm.DataStructure;
using DarkChocoSoft.Exception;
using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmCardManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_RhythmCardPrefab;

        private const string MANAGER_NAME = "[ RhythmCardManager ]";

        private GameObject m_CardPanel;
        private Deque<RhythmCardDto> m_Deck;
        private List<UI_RhythmCard> m_CardHand = new();
        private Queue<UI_RhythmCard> m_CardSelectQueue = new();

        public GameObject CardPanel
        {
            get
            {
                if (m_CardPanel == null)
                {
                    m_CardPanel = GameObject.Find("CardPanel");
                }

                return m_CardPanel;
            }
        }

        public UI_RhythmCard SelectedRhythmCard
        {
            get;
            private set;
        }

        public void DrawCard(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    RhythmCardDto cardData = m_Deck.DequeueFront();

                    DefaultRhythmCardCreator creator = gameObject.GetOrAddComponent<DefaultRhythmCardCreator>();
                    creator.SetPrefab(m_RhythmCardPrefab);
                    creator.SetPositionAndParent(Vector2.zero, CardPanel.transform);
                    creator.SetData(cardData);

                    UI_RhythmCard card = creator.Get();
                    card.SetOnSelectedListener(OnRhythmCardSelect);
                    card.SetOnDeselectedListener(OnRhythmCardDeselect);
                    card.SetOnUseListener(OnRhythmCardUse);

                    m_CardHand.Add(card);
                }
                catch (DequeEmptyException)
                {
                    //TODO: 뽑을 카드가 없으면 GameOver 처리
                    Debug.LogError("뽑을 카드가 없습니다.");
                }
            }
        }

        public RhythmCardDto[] UseRhythmCards()
        {
            if (m_CardSelectQueue == null || m_CardSelectQueue.Count == 0)
            {
                Debug.Log("선택된 카드가 없습니다.");
                return null;
            }

            RhythmCardDto[] temp = new RhythmCardDto[m_CardSelectQueue.Count];
            int i = 0;

            while (m_CardSelectQueue.TryDequeue(out var card))
            {
                UseRhythmCard(card);

                temp[i] = card.Data;
                i++;
            }

            return temp;
        }

        public void UseRhythmCard(UI_RhythmCard card)
        {
            m_CardHand.Remove(card);
            card.Use();
        }

        public void DestroyRhythmCard(UI_RhythmCard card)
        {
            m_CardHand.Remove(card);
            card.Destroy();
        }

        public void DeselectAllCard()
        {
            foreach (UI_RhythmCard card in m_CardHand)
            {
                card.OnDeselected();
            }
        }

        public void DeselectAllCardExcept(UI_RhythmCard except)
        {
            foreach (UI_RhythmCard card in m_CardHand)
            {
                if (card != except)
                    card.OnDeselected();
            }
        }

        private void InitCardDeck()
        {
            m_Deck = GenerateCardDeck(40);
        }

        private Deque<RhythmCardDto> GenerateCardDeck(int count)
        {
            RhythmCardDto[] datas = LoadRhythmCardDto();
            Deque<RhythmCardDto> deck = new();

            for (int i = 0; i < count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, datas.Length);

                deck.EnqueueFront(datas[randomIndex]);
            }

            return deck;
        }

        private void OnRhythmCardSelect(UI_RhythmCard card)
        {
            m_CardSelectQueue.Enqueue(card);
        }

        private void OnRhythmCardDeselect(UI_RhythmCard card)
        {
            m_CardSelectQueue = new Queue<UI_RhythmCard>(m_CardSelectQueue.Where(x => x != card));
        }

        private void OnRhythmCardUse(UI_RhythmCard card)
        {

        }

        private RhythmCardDto[] LoadRhythmCardDto()
        {
            string path = Application.dataPath + "/01. Scripts/Data/Local/Json/Local_RhythmCardData.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                RhythmCardDto[] datas = JsonUtility.FromJson<RhythmCardDtos>(json).datas;

                return datas;
            }
            else
            {
                Debug.LogError("File not found");
            }

            return null;
        }

        private void Awake()
        {
            InitCardDeck();
        }

        // 카드 타입 검색해서 드로우하기 (구현 예정)
        //public void DrawCard(RhythmCardType cardType, int count = 1)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        IProduct product = m_CardFactories[(int)cardType].GetProduct(Vector2.zero, m_CardPanel.transform);
        //    }
        //}
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RhythmCardManager))]
    public class RhythmCardManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RhythmCardManager manager = target as RhythmCardManager;

            if (GUILayout.Button("Get Card 1"))
            {
                manager.DrawCard(1);
            }

            if (GUILayout.Button("Get Card 2"))
            {
                manager.DrawCard(2);
            }

            if (GUILayout.Button("Get Card 3"))
            {
                manager.DrawCard(3);
            }

            if (GUILayout.Button("Get Card 4"))
            {
                manager.DrawCard(4);
            }
        }
    }
#endif
}
