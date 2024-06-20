using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmNoteFactory : Factory
    {
        protected ObjectPool<RhythmNote> m_NotePool;

        public void InitRhythmNote(RhythmNoteData noteData)
        {

        }

        public virtual IRhythmNote GetRhythmNote(Vector2 pos, Transform parent = null)
        {
            RhythmNote note = m_NotePool.Dequeue();

            note.transform.SetParent(parent);
            note.transform.position = pos;
            note.SetOnDestroyListener(OnRhythmNoteDestroy);
            note.StartMove();

            return note;
        }

        public void GenerateRhythmNote(double tempo, RhythmNoteData noteData, Transform parent)
        {
            StartCoroutine(RhythmNoteCoroutine(tempo, noteData, parent));
        }

        protected IEnumerator RhythmNoteCoroutine(double meter, RhythmNoteData noteData, Transform parent)
        {
            double timer = meter;
            int tempoValue = noteData.NoteCount;

            while (noteData.NoteCount > 0)
            {
                timer += Time.deltaTime * tempoValue;

                if (timer >= meter)
                {
                    IRhythmNote note = GetRhythmNote(new Vector3(25f, 1110f, 0), parent);
                    note.InitRhythmNote(noteData);

                    timer -= meter;
                    noteData.NoteCount--;
                }

                yield return null;
            }
        }

        private void OnRhythmNoteDestroy(RhythmNote note)
        {
            m_NotePool.Enqueue(note);
        }

        protected virtual void Init()
        { 
            GameObject notePrefab = BattleSceneGameManager.Instance.SceneData.RhythmNotePrefab;

            InitRhythmNotePool(notePrefab);
        }

        private void InitRhythmNotePool(GameObject notePrefab)
        {
            m_NotePool = new ObjectPool<RhythmNote>()
            { 
                Prefab = notePrefab,
            };
        }

        protected override void Start()
        {
            base.Start();

            Init();
        }
    }
}
