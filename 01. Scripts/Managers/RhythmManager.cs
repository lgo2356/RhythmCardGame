using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmManager : Singleton<RhythmManager>
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
                    UI_RhythmPopup popup = PopupManager.Instance.GetPopup<UI_RhythmPopup>() as UI_RhythmPopup;

                    RhythmNoteConfig config = new()
                    {
                        Count = 1,
                        Speed = NoteSpeed,
                    };

                    if (rhythmCardTypes.TryDequeue(out var rhythmCardType))
                    {
                        StartCoroutine(RhythmNoteCoroutine(tempo, rhythmCardType));
                    }
                    else
                    {
                        break;
                    }

                    IProduct product = m_RhythmNoteFactories[1].GetProduct(popup.RhythmNoteStartPosTransform.position, UICanvas);
                    product.SetConfig(config);

                    timer -= tempo;
                }

                yield return null;
            }
        }

        private IEnumerator RhythmNoteCoroutine(double meter, RhythmCardType rhythmCardType)
        {
            double timer = meter;
            int tempoValue;
            int count;

            switch (rhythmCardType)
            {
                case RhythmCardType.Single:
                    {
                        tempoValue = 1;
                        count = 1;
                    }
                    break;

                case RhythmCardType.Double:
                    {
                        tempoValue = 2;
                        count = 2;
                    }
                    break;

                case RhythmCardType.Triple:
                    {
                        tempoValue = 3;
                        count = 3;
                    }
                    break;

                    //TODO: Long 노트 구현
                case RhythmCardType.Long:
                    {
                        tempoValue = 99;
                        count = 99;
                    }
                    break;

                default:
                    {
                        tempoValue = 0;
                        count = 0;
                    }
                    break;
            }

            while (count > 0)
            {
                timer += Time.deltaTime * tempoValue;

                if (timer >= meter)
                {
                    UI_RhythmPopup popup = PopupManager.Instance.GetPopup<UI_RhythmPopup>() as UI_RhythmPopup;

                    RhythmNoteConfig config = new()
                    {
                        Count = 1,
                        Speed = NoteSpeed,
                    };

                    IProduct product = m_RhythmNoteFactories[0].GetProduct(popup.RhythmNoteStartPosTransform.position, UICanvas);
                    product.SetConfig(config);

                    timer -= meter;
                    count--;
                }

                yield return null;
            }
        }

        private void InitManager()
        {
            RemoveDontDestroyOnLoad();
            SetGameObjectName(MANAGER_NAME);
        }

        private void InitFactory()
        {
            m_RhythmNoteFactories = new Factory[2];

            m_RhythmNoteFactories[0] = gameObject.GetOrAddComponent<RhythmNoteFactory>();
            m_RhythmNoteFactories[1] = gameObject.GetOrAddComponent<RhythmPivotFactory>();
        }

        protected override void Awake()
        {
            base.Awake();

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

            if (GUILayout.Button("Start Rhythm"))
            {
                //rhythmNoteManager.StartRhythm();
            }

            if (GUILayout.Button("Stop Rhythm"))
            {
                rhythmNoteManager.StopRhythm();
            }
        }
    }
#endif
}
