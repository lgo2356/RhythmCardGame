using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class DoubleRhythmCardFactory : RhythmCardFactory
    {
        public override IRhythmCard GetRhythmCard(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmCardPrefab;
            UI_DoubleRhythmCard card = Instantiate(prefab, parent)
                .AddComponent<UI_DoubleRhythmCard>();

            card.LoadConfig("Assets/05. Data/DoubleRhythmCardConfig.asset");

            return card;
        }
    }
}
