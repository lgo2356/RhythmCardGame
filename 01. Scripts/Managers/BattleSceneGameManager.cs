using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;

public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
{
    private const string MANAGER_NAME = "[ BattleSceneGameManager ]";

    public int StageNumber;
    public CharacterType SelectedCharacterType;

    protected override void Awake()
    {
        base.Awake();

        SetupName(MANAGER_NAME);
    }
}
