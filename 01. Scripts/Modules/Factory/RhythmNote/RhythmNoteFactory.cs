using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public abstract class RhythmNoteFactory : Factory
    {
        private ObjectPool<RhythmNote> m_NotePool;

        public abstract void GenerateRhythmNote(double tempo, RhythmNoteConfig config, Transform parent);
        protected abstract IEnumerator RhythmNoteCoroutine(double meter, RhythmNoteConfig config, Transform parent);

        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            RhythmNote note = RhythmNoteObjectPool.Instance.Dequeue();

            //note.transform.SetParent(parent);
            note.transform.position = pos;
            note.StartMove();

            return note;
        }

        protected override void Start()
        {
            base.Start();

            RhythmNoteObjectPool.Instance.transform.SetParent(transform.parent);
            RhythmNoteObjectPool.Instance.Prefab = BattleSceneGameManager.Instance.SceneData.RhythmNotePrefab;
        }
    }
}
