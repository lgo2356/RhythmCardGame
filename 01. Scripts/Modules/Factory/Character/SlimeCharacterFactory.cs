using DarkChocoSoft.RhythmCardGame.Character;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class SlimeCharacterFactory : CharacterFactory
    {
        public override ICharacter GetCharacter(Vector2 pos, Transform parent)
        {
            GameObject prefab = BattleSceneGameManager.Instance.SceneData.PlayerCharacterPrefab;
            SlimeCharacter character = Instantiate(prefab, parent)
                .AddComponent<SlimeCharacter>();

            character.LoadConfig("Assets/05. Data/Character/SlimeCharacterConfig.asset");

            return character;
        }
    }
}
