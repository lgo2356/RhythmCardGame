using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class RhythmManager : MonoBehaviour
    {
        [SerializeField] EndRhythmObject m_EndRhythmNotePrefab;
        [SerializeField] GameObject m_RhythmNotePrefab;
        [SerializeField] RectTransform m_RhythmNoteStartPositionTransform;
        [SerializeField] RhythmNoteHitTimingManager m_RhythmNoteHitTimingManager;

        public int BPM = 120;
        public int NoteSpeed = 400;

        private Transform m_UICanvas;
        private Coroutine m_GenerateRhythmNoteCoroutine;
        private Action m_OnRhythmStartAction;
        private Action m_OnRhythmStopAction;

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

        public void StartRhythm(RhythmNoteDto[] rhythmDatas)
        {
            UI_RhythmPopup popup = PopupManager.Instance.GetPopup<UI_RhythmPopup>(PopupType.UI_RhythmPopup);

            popup.SetOnShowListener(() =>
            {
                m_GenerateRhythmNoteCoroutine = StartCoroutine(BeatCoroutine(rhythmDatas, popup));
            });
            popup.Show();
        }

        public void StopRhythm()
        {
            StopCoroutine(m_GenerateRhythmNoteCoroutine);
            m_GenerateRhythmNoteCoroutine = null;

            if (PopupManager.Instance.IsShowing(PopupType.UI_RhythmPopup))
            {
                PopupManager.Instance.HidePopup(PopupType.UI_RhythmPopup);
            }

            //float ratio = m_RhythmNoteHitTimingManager.HitRatio / 100f;

            m_OnRhythmStopAction?.Invoke();

            //BattleSceneGameManager.Instance.DoBattle(ratio);
            BattleSceneGameManager.Instance.DoBattle(1f);
        }

        public void SetOnRhythmStartListener(Action callback)
        {
            m_OnRhythmStartAction -= callback;
            m_OnRhythmStartAction += callback;
        }

        public void SetOnRhythmStopListener(Action callback)
        {
            m_OnRhythmStopAction -= callback;
            m_OnRhythmStopAction += callback;
        }

        private IEnumerator BeatCoroutine(RhythmNoteDto[] notes, UI_RhythmPopup popup)
        {
            m_OnRhythmStartAction?.Invoke();

            double timer = 0d;
            double tempo = 60d / BPM;

            RhythmNoteData noteData = new()
            {
                Speed = NoteSpeed,
            };

            int i = 0;

            while (i < notes.Length)
            {
                timer += Time.deltaTime;

                if (timer >= tempo)
                {
                    switch (notes[i].type)
                    {
                        case "normal":
                            {
                                noteData.NoteCount = notes[i].count;

                                StartCoroutine(GenerateNoteCoroutine(notes[i], tempo, popup));
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

            //Debug.Log($"Beat End ratio : {m_RhythmNoteHitTimingManager.HitRatio}");

            EndRhythmObject endRhythmNote = Instantiate(m_EndRhythmNotePrefab, UICanvas);
            endRhythmNote.transform.position = new Vector2(25f, 1110f);
            endRhythmNote.Speed = noteData.Speed;
            endRhythmNote.SetOnDestroyListener(OnEndRhythmObjectDestory);
            endRhythmNote.StartMove();
        }

        private IEnumerator GenerateNoteCoroutine(RhythmNoteDto noteData, double tempo, UI_RhythmPopup popup)
        {
            double timer = tempo;
            int tempoValue = noteData.count;

            while (noteData.count > 0)
            {
                timer += Time.deltaTime * tempoValue;

                if (timer >= tempo)
                {
                    DefaultRhythmNoteCreator creator = gameObject.GetOrAddComponent<DefaultRhythmNoteCreator>();
                    creator.SetPrefab(m_RhythmNotePrefab);
                    creator.SetData(noteData);

                    RhythmNote note = creator.Get();
                    note.StartMove(NoteSpeed);

                    popup.InjectNote(note);

                    timer -= tempo;
                    noteData.count--;
                }

                yield return null;
            }
        }

        private void OnEndRhythmObjectDestory()
        {
            StopRhythm();
        }

        private void OnRhythmPopupShow()
        {
            
        }

        private void OnRhythmPopupHide()
        {

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
                    //rhythmNoteManager.StartTestRhythm();
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
