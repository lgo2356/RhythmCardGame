using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Character;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class CatCharacterFactory : CharacterFactory
    {
        public override ICharacter GetCharacter(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.MonsterCharacterPrefab;
            CatCharacter character = Instantiate(prefab, parent)
                .AddComponent<CatCharacter>();

            character.LoadConfig("Assets/05. Data/Character/CatCharacterConfig.asset");

            return character;
        }
    }
}
