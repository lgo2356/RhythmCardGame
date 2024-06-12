using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmManager : MonoBehaviour
    {
        [SerializeField] GameObject m_RhythmPivotPrefab;

        private const string MANAGER_NAME = "[ RhythmManager ]";

        public int BPM = 120;

        private Transform m_UICanvas;
        private Coroutine m_GenerateRhythmNoteCoroutine;
        private Factory[] m_RhythmNoteFactories;
        private bool m_IsRhythmStarted = false;
        private int m_NoteSpeed = 800;

        public Transform UICanvas
        {
            get
            {
                if (m_UICanvas == null)
                {
                    m_UICanvas = GameObject.Find("[ UI ]").transform;
                }

                return m_UICanvas;
            }
        }

        public int NoteSpeed
        {
            get
            {
                return m_NoteSpeed;
            }

            private set 
            { 
                m_NoteSpeed = value; 
            }
        }

        public void StartRhythm()
        {
            Dictionary<int, RhythmCardType> cardTypeDic = BattleSceneGameManager.Instance.RhythmCardComboDic;
            Queue<RhythmCardType> rhythmCardTypes = new();

            for (int i = 0; i < cardTypeDic.Count; i++) 
            {
                rhythmCardTypes.Enqueue(cardTypeDic[i]);
            }

            m_GenerateRhythmNoteCoroutine = StartCoroutine(TempoCoroutine(rhythmCardTypes));
        }

        public void StartTestRhythm()
        {
            Queue<RhythmCardType> rhythmCardTypes = new();

            for (int i = 0; i < 10; i++)
            {
                rhythmCardTypes.Enqueue(RhythmCardType.Single);
            }

            m_GenerateRhythmNoteCoroutine = StartCoroutine(TempoCoroutine(rhythmCardTypes));
        }

        public void StopRhythm()
        {
            StopCoroutine(m_GenerateRhythmNoteCoroutine);

            m_GenerateRhythmNoteCoroutine = null;
        }

        private IEnumerator TempoCoroutine(Queue<RhythmCardType> rhythmCardTypes)
        {
            double timer = 0d;
            double tempo = 60d / BPM;

            while (true)
            {
                timer += Time.deltaTime;

                if (timer >= tempo)
                {
                    RhythmNoteConfig config = new()
                    {
                        Count = 1,
                        Speed = NoteSpeed,
                    };

                    if (rhythmCardTypes.TryDequeue(out var rhythmCardType))
                    {
                        switch (rhythmCardType)
                        {
                            case RhythmCardType.Single:
                                {
                                    SingleRhythmNoteFactory factory = m_RhythmNoteFactories[0] as SingleRhythmNoteFactory;
                                    factory.GenerateRhythmNote(tempo, config, UICanvas);
                                }
                                break;

                            case RhythmCardType.Double:
                                {
                                    DoubleRhythmNoteFactory factory = m_RhythmNoteFactories[1] as DoubleRhythmNoteFactory;
                                    factory.GenerateRhythmNote(tempo, config, UICanvas);
                                }
                                break;

                            case RhythmCardType.Triple:
                                {
                                    TripleRhythmNoteFactory factory = m_RhythmNoteFactories[2] as TripleRhythmNoteFactory;
                                    factory.GenerateRhythmNote(tempo, config, UICanvas);
                                }
                                break;

                            case RhythmCardType.Long:
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }

                    IProduct product = m_RhythmNoteFactories[3].GetProduct(new Vector3(25f, 1110f, 0), UICanvas);
                    product.SetConfig(config);

                    timer -= tempo;
                }

                yield return null;
            }
        }

        private void InitManager()
        {
            //RemoveDontDestroyOnLoad();
            //SetGameObjectName(MANAGER_NAME);
        }

        private void InitFactory()
        {
            m_RhythmNoteFactories = new Factory[4];

            m_RhythmNoteFactories[0] = gameObject.GetOrAddComponent<SingleRhythmNoteFactory>();
            m_RhythmNoteFactories[1] = gameObject.GetOrAddComponent<DoubleRhythmNoteFactory>();
            m_RhythmNoteFactories[2] = gameObject.GetOrAddComponent<TripleRhythmNoteFactory>();
            m_RhythmNoteFactories[3] = gameObject.GetOrAddComponent<RhythmPivotFactory>();
        }

        void Awake()
        {
            //base.Awake();

            InitManager();
            InitFactory();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RhythmManager))]
    public class RhythmNoteManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RhythmManager rhythmNoteManager = target as RhythmManager;

            if (GUILayout.Button("Test Rhythm"))
            {
                rhythmNoteManager.StartTestRhythm();
            }

            if (GUILayout.Button("Stop Rhythm"))
            {
                rhythmNoteManager.StopRhythm();
            }
        }
    }
#endif
}
