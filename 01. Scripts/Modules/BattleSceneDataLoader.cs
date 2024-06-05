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

            Debug.Log($"Stage Number: {dataHolder.Data.StageNumber}");
            Debug.Log($"Character Type: {dataHolder.Data.CharacterType}");
        }
    }
}
