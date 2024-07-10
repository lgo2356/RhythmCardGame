using DarkChocoSoft.RhythmCardGame.Data;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public abstract class RhythmNoteCreator : MonoBehaviour
    {
        protected ObjectPool<RhythmNote> m_NotePool;
        protected RhythmNoteDto m_Data;
        protected GameObject m_Prefab;

        public abstract void SetData(RhythmNoteDto data);
        public abstract void SetPrefab(GameObject prefab);
        protected abstract RhythmNote Create();

        public RhythmNote Get()
        {
            if (m_Data == null)
                throw new System.Exception("Data is not set.");

            if (m_Prefab == null)
                throw new System.Exception("Prefab is not set.");

            RhythmNote note = Create();

            return note;
        }
    }
}
