using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class BattleSceneDataLoader : DataLoader
    {
        protected override void Start()
        {
            base.Start();

            int stageNumber = PlayerPrefs.GetInt("StageNumber", -1);
            string characterType = PlayerPrefs.GetString("CharacterType", string.Empty);

            Debug.Log($"Stage Number: {stageNumber}");
            Debug.Log($"Character Type: {characterType}");
        }
    }
}
