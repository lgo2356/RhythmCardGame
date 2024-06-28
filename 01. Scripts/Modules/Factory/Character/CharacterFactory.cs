using DarkChocoSoft.RhythmCardGame;
using DarkChocoSoft.RhythmCardGame.Module;
using UnityEngine;

public abstract class CharacterFactory : Factory
{
    public abstract Character GetCharacter(GameObject prefab, Vector2 pos, Transform parent);

    protected override void Awake()
    {

    }

    protected override void Start()
    {

    }
}
