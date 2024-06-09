using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class LongRhythmCardFactory : Factory
    {
        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmCardPrefab;
            UI_LongRhythmCard card = Instantiate(prefab, parent)
                .AddComponent<UI_LongRhythmCard>();

            card.LoadConfig("Assets/05. Data/LongRhythmCardConfig.asset");

            return card;
        }
    }
}
