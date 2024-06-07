using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class SingleRhythmCardFactory : Factory
    {
        private GameObject m_RhythmCardPrefab;

        public override IProduct GetProduct(Vector2 pos, Transform parent)
        {
            UI_SingleRhythmCard card = Instantiate(m_RhythmCardPrefab, parent)
                .AddComponent<UI_SingleRhythmCard>();

            card.LoadConfig("Assets/05. Data/SingleRhythmCardConfig.asset");

            return card;
        }

        protected override void Awake()
        {
            ResourceManager.Instance.LoadAsync<GameObject>("Assets/04. Prefabs/UI_RhythmCard.prefab", (prefab) =>
            {
                m_RhythmCardPrefab = prefab;
            });
        }
    }
}
