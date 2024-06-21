using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Module;
using UnityEngine;

public abstract class RhythmCardFactory : Factory
{
    public abstract IRhythmCard GetRhythmCard(Vector2 pos, Transform parent);

    protected override void Awake()
    {

    }

    protected override void Start()
    {

    }
}
