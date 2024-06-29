using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmManager : MonoBehaviour
    {
        [SerializeField] EndRhythmObject m_EndRhythmNotePrefab;
        [SerializeField] RectTransform m_RhythmNoteStartPositionTransform;
        [SerializeField] RhythmNoteHitTimingManager m_RhythmNoteHitTimingManager;

        public int BPM = 120;

        private Transform m_UICanvas;
        private Coroutine m_GenerateRhythmNoteCoroutine;
        private RhythmNoteFactory[] m_RhythmNoteFactories;
        private int m_NoteSpeed = 400;
        private Action m_EndRhythmNoteDestroyAction;

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
            RhythmCardData cardData = BattleSceneGameManager.Instance.SelectedCard;

            //TODO: 리듬 난이도에 따라 비트 선택하기

            BeatData beatData = GetDummyBeat();

            m_GenerateRhythmNoteCoroutine = StartCoroutine(BeatCoroutine(beatData));
        }

        public void StopRhythm()
        {
            StopCoroutine(m_GenerateRhythmNoteCoroutine);
            m_GenerateRhythmNoteCoroutine = null;

            if (PopupManager.Instance.IsShowing(PopupType.UI_RhythmPopup))
            {
                PopupManager.Instance.HidePopup(PopupType.UI_RhythmPopup);
            }

            float ratio = m_RhythmNoteHitTimingManager.HitRatio / 100f;
            BattleSceneGameManager.Instance.DoBattle(ratio);
        }

        public void StartTestRhythm()
        {
            BeatData beatData = GetTestBeat();

            m_GenerateRhythmNoteCoroutine = StartCoroutine(BeatCoroutine(beatData));
        }

        private IEnumerator BeatCoroutine(BeatData beatData)
        {
            Vector2 noteStartPosition = m_RhythmNoteStartPositionTransform.position;

            double timer = 0d;
            double tempo = 60d / BPM;

            RhythmNoteData noteData = new()
            {
                Speed = NoteSpeed,
            };

            int i = 0;

            while (i < beatData.total)
            {
                timer += Time.deltaTime;

                if (timer >= tempo)
                {
                    switch (beatData.notes[i].type)
                    {
                        case "normal":
                            {
                                noteData.NoteCount = beatData.notes[i].count;

                                RhythmNoteFactory factory = m_RhythmNoteFactories[0];
                                factory.GenerateRhythmNote(tempo, noteData, noteStartPosition, UICanvas);
                            }
                            break;

                        case "long":
                            break;
                    }

                    i++;
                    timer -= tempo;
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            Debug.Log($"Beat End ratio : {m_RhythmNoteHitTimingManager.HitRatio}");

            EndRhythmObject endRhythmNote = Instantiate(m_EndRhythmNotePrefab, UICanvas);
            endRhythmNote.transform.position = noteStartPosition;
            endRhythmNote.Speed = noteData.Speed;
            endRhythmNote.SetOnDestroyListener(OnEndRhythmObjectDestory);
            endRhythmNote.StartMove();
        }

        private void InitFactory()
        {
            m_RhythmNoteFactories = new RhythmNoteFactory[4];

            m_RhythmNoteFactories[0] = gameObject.GetOrAddComponent<NormalRhythmNoteFactory>();
            m_RhythmNoteFactories[0].Init();

            m_RhythmNoteFactories[1] = gameObject.GetOrAddComponent<LongRhythmNoteFactory>();
            m_RhythmNoteFactories[1].Init();

            //m_RhythmNoteFactories[2] = gameObject.GetOrAddComponent<RhythmPivotFactory>();

            //TODO : 각 팩토리에 필요한 데이터 주입하기
        }

        private void OnEndRhythmObjectDestory()
        {
            StopRhythm();
        }

        void Awake()
        {
            InitFactory();
        }

        private BeatData GetDummyBeat()
        {
            string path = Application.dataPath + "/01. Scripts/Data/DummyBeatJson.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                BeatData data = JsonUtility.FromJson<BeatData>(json);

                return data;
            }
            else
            {
                Debug.LogError("File not found");
            }

            return null;
        }

        private BeatData GetTestBeat()
        {
            string path = Application.dataPath + "/01. Scripts/Data/TestBeatJson.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                BeatData data = JsonUtility.FromJson<BeatData>(json);

                return data;
            }
            else
            {
                Debug.LogError("File not found");
            }

            return null;
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
}
