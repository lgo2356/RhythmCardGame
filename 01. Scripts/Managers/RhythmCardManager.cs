using DarkChocoSoft.Algorithm.DataStructure;
using DarkChocoSoft.Exception;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmCardManager : MonoBehaviour
    {
        private const string MANAGER_NAME = "[ RhythmCardManager ]";

        private GameObject m_CardPanel;
        private RhythmCardFactory[] m_CardFactories;
        private Deque<RhythmCardType> m_CardDeck;
        private List<UI_RhythmCard> m_CardHand = new();

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
                    RhythmCardType cardType = m_CardDeck.DequeueFront();
                    IRhythmCard character = m_CardFactories[(int)cardType].GetRhythmCard(Vector2.zero, CardPanel.transform);
                    UI_RhythmCard card = character as UI_RhythmCard;
                    card.SetOnSelectedListener(OnRhythmCardSelected);
                    card.SetOnDeselectedListener(OnRhythmCardDeselected);
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

        private void InitCardFactory()
        {
            m_CardFactories = new RhythmCardFactory[Enum.GetValues(typeof(RhythmCardType)).Length];

            m_CardFactories[0] = gameObject.GetOrAddComponent<SingleRhythmCardFactory>();
            m_CardFactories[1] = gameObject.GetOrAddComponent<DoubleRhythmCardFactory>();
            m_CardFactories[2] = gameObject.GetOrAddComponent<TripleRhythmCardFactory>();
            m_CardFactories[3] = gameObject.GetOrAddComponent<LongRhythmCardFactory>();
        }

        private void InitCardDeck()
        {
            m_CardDeck = GenerateCardDeck(40);
        }

        private Deque<RhythmCardType> GenerateCardDeck(int count)
        {
            Deque<RhythmCardType> cardDeck = new();
            int cardTypeCount = Enum.GetValues(typeof(RhythmCardType)).Length;

            for (int i = 0; i < count; i++)
            {
                //RhythmCardType randomCard = (RhythmCardType)UnityEngine.Random.Range(0, cardTypeCount);
                RhythmCardType randomCard = (RhythmCardType)UnityEngine.Random.Range(0, 1);
                //RhythmCardType randomCard = (RhythmCardType)UnityEngine.Random.Range(3, 4);

                cardDeck.EnqueueFront(randomCard);
            }

            return cardDeck;
        }

        private void OnRhythmCardSelected(UI_RhythmCard card)
        {
            SelectedRhythmCard = card;
        }

        private void OnRhythmCardDeselected(UI_RhythmCard card)
        {
            SelectedRhythmCard = null;
        }

        private void OnRhythmCardUse(UI_RhythmCard card)
        {

        }

        private void Awake()
        {
            InitCardFactory();
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
