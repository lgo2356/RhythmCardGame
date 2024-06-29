using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class SlimeCharacterFactory : CharacterFactory
    {
        public override Character GetCharacter(GameObject prefab, Vector2 pos, Transform parent)
        {
            SlimeCharacter slime = Instantiate(prefab, parent)
                .AddComponent<SlimeCharacter>();

            slime.transform.SetParent(parent);
            slime.transform.position = pos;
            slime.LoadConfig("Assets/05. Data/Character/SlimeCharacterConfig.asset");
            slime.InitStat();

            return slime;
        }
    }
}
