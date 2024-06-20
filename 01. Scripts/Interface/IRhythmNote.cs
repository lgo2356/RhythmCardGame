namespace DarkChocoSoft.RhythmCardGame.Interface
{
    public struct RhythmNoteData
    {
        public int Speed;
        public int NoteCount;
    }

    public interface IRhythmNote : IProduct
    {
        public void InitRhythmNote(RhythmNoteData data);
    }
}
