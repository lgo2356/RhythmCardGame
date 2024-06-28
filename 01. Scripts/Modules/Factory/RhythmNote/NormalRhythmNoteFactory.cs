using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class NormalRhythmNoteFactory : RhythmNoteFactory
    {
        public override void Init()
        {
            GameObject notePrefab = BattleSceneGameManager.Instance.SceneData.RhythmNotePrefab;

            InitRhythmNotePool(notePrefab);
        }

        public override IRhythmNote GetRhythmNote(RhythmNoteData data, Vector2 pos, Transform parent = null)
        {
            NormalRhythmNote note = m_NotePool.Dequeue() as NormalRhythmNote;
            note.transform.SetParent(parent);
            note.transform.position = pos;
            note.InitRhythmNote(data);
            note.SetOnDestroyListener(OnRhythmNoteDestroy);
            note.StartMove();

            return note;
        }

        public override void GenerateRhythmNote(double tempo, RhythmNoteData noteData, Vector2 position, Transform parent)
        {
            StartCoroutine(RhythmNoteCoroutine(tempo, noteData, parent));
        }

        protected virtual IEnumerator RhythmNoteCoroutine(double meter, RhythmNoteData noteData, Transform parent)
        {
            double timer = meter;
            int tempoValue = noteData.NoteCount;

            while (noteData.NoteCount > 0)
            {
                timer += Time.deltaTime * tempoValue;

                if (timer >= meter)
                {
                    IRhythmNote note = GetRhythmNote(noteData, new Vector3(25f, 1110f, 0), parent);

                    timer -= meter;
                    noteData.NoteCount--;
                }

                yield return null;
            }
        }

        private void InitRhythmNotePool(GameObject notePrefab)
        {
            m_NotePool = new ObjectPool<RhythmNote>()
            {
                Prefab = notePrefab,
            };
        }
    }
}
