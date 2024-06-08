namespace DarkChocoSoft.RhythmCardGame.Const
{
    /**
     * 1줄 플레이어
     * 2줄 몬스터
     */
    public enum CharacterType
    {
        Slime,
        Cat,
    }

    public enum RhythmCardType
    {
        Single = 0,
        Double = 1,
        Triple = 2,
        Long = 3,
    }

    public enum SceneName
    {
        MainScene,
        LobbyScene,
        BattleScene,
    }

    public static class AssetPath
    {
        public const string LOADING_SCREEN_PATH = "Assets/04. Prefabs/UI/UI_LoadingScreen.prefab";
    }
}
