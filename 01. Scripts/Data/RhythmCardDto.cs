namespace DarkChocoSoft.RhythmCardGame.Data
{
    /**
     * 캐릭터가 사용할 수 있는 카드 풀은 Json 파일별로 나눈다.
     */

    [System.Serializable]
    public class RhythmCardDtos
    {
        public RhythmCardDto[] datas;
    }

    [System.Serializable]
    public class RhythmCardDto
    {
        public long id;
        public string config_path;
        public string name;
        public string description;
        public int damage;
        public RhythmNoteDto[] notes;
    }

    [System.Serializable]
    public class RhythmNoteDto
    {
        public string type;
        public int count;
    }
}
