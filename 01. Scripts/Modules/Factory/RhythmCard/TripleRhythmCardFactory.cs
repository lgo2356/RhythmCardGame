using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class TripleRhythmCardFactory : RhythmCardFactory
    {
        public override IRhythmCard GetRhythmCard(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.RhythmCardPrefab;
            UI_TripleRhythmCard card = Instantiate(prefab, parent)
                .AddComponent<UI_TripleRhythmCard>();

            card.LoadConfig("Assets/05. Data/TripleRhythmCardConfig.asset");

            return card;
        }
    }
}
