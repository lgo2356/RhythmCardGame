using DarkChocoSoft.RhythmCardGame.Interface;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public abstract class RhythmNoteFactory : Factory
    {
        protected ObjectPool<RhythmNote> m_NotePool;

        public abstract IRhythmNote GetRhythmNote(RhythmNoteData data, Vector2 pos, Transform parent = null);
        public abstract void GenerateRhythmNote(double tempo, RhythmNoteData noteData, Vector2 position, Transform parent);
        public abstract void Init();

        protected void OnRhythmNoteDestroy(RhythmNote note)
        {
            m_NotePool.Enqueue(note);
        }
    }
}
