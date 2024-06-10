using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmNoteFactory : Factory
    {
        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmNotePrefab;
            RhythmNote note = Instantiate(prefab, parent)
                .GetOrAddComponent<RhythmNote>();

            note.transform.position = pos;
            note.StartMove();

            return note;
        }
    }
}
