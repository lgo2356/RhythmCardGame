using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmPivotFactory : Factory
    {
        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmPivotPrefab;
            RhythmNote note = Instantiate(prefab, parent)
                .GetOrAddComponent<RhythmNote>();

            note.transform.position = pos;
            note.StartMove();
            //note.LoadConfig("Assets/05. Data/RhythmNote/RhythmNoteConfig.asset");

            return note;
        }
    }
}
