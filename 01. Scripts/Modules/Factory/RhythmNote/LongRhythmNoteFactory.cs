using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class LongRhythmNoteFactory : RhythmNoteFactory
    {
        public override void Init()
        {
            GameObject notePrefab = BattleSceneGameManager.Instance.SceneData.LongRhythmNotePrefab;

            InitRhythmNotePool(notePrefab);
        }

        public override IRhythmNote GetRhythmNote(RhythmNoteData data, Vector2 pos, Transform parent = null)
        {
            LongRhythmNote note = m_NotePool.Dequeue() as LongRhythmNote;
            note.transform.SetParent(parent);
            note.transform.position = pos;
            note.InitRhythmNote(data);
            note.SetOnDestroyListener(OnRhythmNoteDestroy);
            note.StartMove();

            return note;
        }

        public override void GenerateRhythmNote(double tempo, RhythmNoteData noteData, Vector2 position, Transform parent)
        {
            StartCoroutine(GenerateRhythmNoteCoroutine(tempo, noteData, parent));
        }

        private IEnumerator GenerateRhythmNoteCoroutine(double tempo, RhythmNoteData noteData, Transform parent)
        {
            double timer = 0d;

            while (true)
            {
                timer += Time.deltaTime;

                if (timer >= 1)
                {
                    StartCoroutine(RhythmNoteCoroutine(tempo, noteData, parent));

                    timer -= 1;
                    noteData.NoteCount -= 1;
                }

                if (noteData.NoteCount <= 0)
                {
                    break;
                }

                yield return null;
            }
        }

        private IEnumerator RhythmNoteCoroutine(double meter, RhythmNoteData noteData, Transform parent)
        {
            double timer = meter;
            int tempoValue = noteData.NoteCount;

            while (noteData.NoteCount > 0)
            {
                timer += Time.deltaTime * tempoValue;

                if (timer >= meter)
                {
                    IRhythmNote note = GetRhythmNote(noteData, new Vector3(25f, 1110f, 0), parent);
                    LongRhythmNote longRhythmNote = note as LongRhythmNote;
                    longRhythmNote.SetNoteLength(noteData.Speed * (float)meter);

                    timer -= meter;
                    noteData.NoteCount--;
                }
            }

            yield return null;
        }

        private void OnRhythmNoteDestroy(LongRhythmNote note)
        {
            m_NotePool.Enqueue(note);
        }

        private void InitRhythmNotePool(GameObject prefab)
        {
            m_NotePool = new ObjectPool<RhythmNote>()
            {
                Prefab = prefab,
            };
        }
    }
}
