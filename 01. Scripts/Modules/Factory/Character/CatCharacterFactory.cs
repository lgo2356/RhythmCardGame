using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class CatCharacterFactory : CharacterFactory
    {
        public override Character GetCharacter(GameObject prefab, Vector2 pos, Transform parent)
        {
            CatCharacter cat = Instantiate(prefab, parent)
                .AddComponent<CatCharacter>();

            cat.transform.SetParent(parent);
            cat.transform.position = pos;
            cat.LoadConfig("Assets/05. Data/Character/CatCharacterConfig.asset");
            cat.InitStat();

            return cat;
        }
    }
}
