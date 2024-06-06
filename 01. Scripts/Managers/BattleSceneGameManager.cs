using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
{
    private const string MANAGER_NAME = "[ BattleSceneGameManager ]";

    public int StageNumber;
    public CharacterType SelectedCharacterType;

    public override void Awake()
    {
        base.Awake();

        SetupName(MANAGER_NAME);
    }
}
