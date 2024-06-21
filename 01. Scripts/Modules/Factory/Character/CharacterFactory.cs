using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using UnityEngine;

public abstract class CharacterFactory : Factory
{
    public abstract ICharacter GetCharacter(Vector2 pos, Transform parent);

    protected override void Awake()
    {

    }

    protected override void Start()
    {

    }
}
