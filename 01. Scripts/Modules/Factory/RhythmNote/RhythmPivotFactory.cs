using DarkChocoSoft.RhythmCardGame.Manager;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmPivotFactory : RhythmNoteFactory
    {
        protected override void Start()
        {
            base.Start();

            m_NotePool.Prefab = BattleSceneGameManager.Instance.SceneData.RhythmPivotPrefab;
        }
    }
}
