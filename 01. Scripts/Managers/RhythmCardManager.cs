using DarkChocoSoft.Algorithm.DataStructure;
using DarkChocoSoft.Exception;
using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using System;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmCardManager : Singleton<RhythmCardManager>
    {
        private const string MANAGER_NAME = "[ RhythmCardManager ]";

        private GameObject m_CardPanel;
        private Factory[] m_CardFactories;
        private Deque<RhythmCardType> m_CardDeck;

        public void DrawCard(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    RhythmCardType cardType = m_CardDeck.DequeueFront();
                    IProduct product = m_CardFactories[(int)cardType].GetProduct(Vector2.zero, m_CardPanel.transform);
                    //IProduct product = m_CardFactories[0].GetProduct(Vector2.zero, m_CardPanel.transform);
                }
                catch (DequeEmptyException)
                {
                    //TODO: 뽑을 카드가 없으면 GameOver 처리
                    Debug.LogError("뽑을 카드가 없습니다.");
                }
            }
        }

        public bool IsRead
        {
            get; private set;
        }

        private void InitManager()
        {
            RemoveDontDestroyOnLoad();
            SetGameObjectName(MANAGER_NAME);
        }

        private void InitCardFactory()
        {
            m_CardFactories = new Factory[Enum.GetValues(typeof(RhythmCardType)).Length];

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
                RhythmCardType randomCard = (RhythmCardType)UnityEngine.Random.Range(0, cardTypeCount);

                cardDeck.EnqueueFront(randomCard);
            }

            return cardDeck;
        }

        protected override void Awake()
        {
            base.Awake();

            InitManager();
            InitCardFactory();
            InitCardDeck();

            m_CardPanel = GameObject.Find("CardPanel");
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
