using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
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
        private RhythmNoteFactory[] m_RhythmNoteFactories;
        private bool m_IsRhythmStarted = false;
        private int m_NoteSpeed = 400;

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

            for (int i = 0; i < 100; i++)
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
                    RhythmNoteData noteData = new()
                    {
                        Speed = NoteSpeed,
                    };

                    if (rhythmCardTypes.TryDequeue(out var rhythmCardType))
                    {
                        switch (rhythmCardType)
                        {
                            case RhythmCardType.Single:
                                {
                                    noteData.NoteCount = 1;
                                    m_RhythmNoteFactories[0].GenerateRhythmNote(tempo, noteData, UICanvas);
                                }
                                break;

                            case RhythmCardType.Double:
                                {
                                    noteData.NoteCount = 2;
                                    m_RhythmNoteFactories[0].GenerateRhythmNote(tempo, noteData, UICanvas);
                                }
                                break;

                            case RhythmCardType.Triple:
                                {
                                    noteData.NoteCount = 3;
                                    m_RhythmNoteFactories[0].GenerateRhythmNote(tempo, noteData, UICanvas);
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

                    //IRhythmNote note = m_RhythmNoteFactories[1].GetRhythmNote(new Vector3(25f, 1110f, 0), UICanvas);
                    //note.InitRhythmNote(noteData);

                    timer -= tempo;
                }

                yield return null;
            }
        }

        private void InitFactory()
        {
            m_RhythmNoteFactories = new RhythmNoteFactory[4];

            m_RhythmNoteFactories[0] = gameObject.GetOrAddComponent<RhythmNoteFactory>();
            m_RhythmNoteFactories[1] = gameObject.GetOrAddComponent<RhythmPivotFactory>();

            //TODO : 각 팩토리에 필요한 데이터 주입하기
        }

        void Awake()
        {
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
