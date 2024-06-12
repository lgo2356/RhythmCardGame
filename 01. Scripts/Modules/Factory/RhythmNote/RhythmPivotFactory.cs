using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmPivotFactory : Factory
    {
        private ObjectPool<RhythmNote> m_NotePool;

        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            RhythmNote note = RhythmPivotObjectPool.Instance.Dequeue();

            //note.transform.SetParent(parent);
            note.transform.position = pos;
            note.StartMove();

            return note;
        }

        protected override void Start()
        {
            base.Start();

            RhythmPivotObjectPool.Instance.transform.SetParent(transform.parent);
            RhythmPivotObjectPool.Instance.Prefab = BattleSceneGameManager.Instance.SceneData.RhythmPivotPrefab;
        }
    }
}
