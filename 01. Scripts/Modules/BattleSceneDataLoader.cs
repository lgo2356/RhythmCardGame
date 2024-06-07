using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class BattleSceneDataLoader : DataLoader
    {
        public bool TestMode = false;

        private void DataLoad()
        {
            int stageNumber = PlayerPrefs.GetInt("StageNumber", -1);
            string characterType = PlayerPrefs.GetString("CharacterType", string.Empty);

            Debug.Log($"Stage Number: {stageNumber}");
            Debug.Log($"Character Type: {characterType}");

            BattleSceneGameManager.Instance.StageNumber = stageNumber;
            BattleSceneGameManager.Instance.SelectedCharacterType = (CharacterType)System.Enum.Parse(typeof(CharacterType), characterType);
        }

        private void TestDataLoad()
        {
            BattleSceneGameManager.Instance.StageNumber = 1;
            BattleSceneGameManager.Instance.SelectedCharacterType = CharacterType.Slime;
        }

        protected override void Start()
        {
            base.Start();

            if (TestMode)
            {
                TestDataLoad();
            }
            else
            {
                DataLoad();
            }
        }
    }
}
