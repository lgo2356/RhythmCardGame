using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class SingleRhythmCardFactory : RhythmCardFactory
    {
        public override IRhythmCard GetRhythmCard(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmCardPrefab;
            UI_SingleRhythmCard card = Instantiate(prefab, parent)
                .AddComponent<UI_SingleRhythmCard>();

            card.LoadConfig("Assets/05. Data/SingleRhythmCardConfig.asset");

            return card;
        }
    }
}
