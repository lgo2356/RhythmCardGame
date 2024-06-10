using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Data
{
    [CreateAssetMenu(fileName = "RhythmNoteConfig", menuName = "ScriptableObjects/RhythmNoteConfig")]
    public class RhythmNoteConfig : ScriptableObject
    {
        public int Count;
        public int Speed;
    }
}
