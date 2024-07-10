using DarkChocoSoft.RhythmCardGame.Data;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class DefaultRhythmNoteCreator : RhythmNoteCreator
    {
        public override void SetData(RhythmNoteDto data)
        {
            m_Data = data;
        }

        public override void SetPrefab(GameObject prefab)
        {
            m_Prefab = prefab;
        }

        protected override RhythmNote Create()
        {
            if (m_NotePool == null)
            {
                m_NotePool = new ObjectPool<RhythmNote>()
                {
                    Prefab = m_Prefab,
                };
            }

            RhythmNote result = m_NotePool.Dequeue();

            result.SetData(m_Data);
            result.SetOnDestroyListener((note) =>
            {
                m_NotePool.Enqueue(note);
            });

            return result;
        }
    }
}
