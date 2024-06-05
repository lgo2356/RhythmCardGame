using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class BattleSceneDataLoader : DataLoader
    {
        protected override void Start()
        {
            base.Start();

            StageDataHolder dataHolder = FindObjectOfType<StageDataHolder>();

            if (dataHolder == null)
            {
                throw new System.Exception("StageDataHolder is not found.");
            }

            int stageNumber = dataHolder.Data.StageNumber;

            Debug.Log($"Stage Number: {stageNumber}");
        }
    }
}
