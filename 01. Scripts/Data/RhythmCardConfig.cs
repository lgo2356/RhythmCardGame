using DarkChocoSoft.RhythmCardGame.Const;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Data
{
    [CreateAssetMenu(fileName = "RhythmCardConfig", menuName = "ScriptableObjects/RhythmCardConfig")]
    public class RhythmCardConfig : ScriptableObject
    {
        public RhythmCardType CardType;
        public Sprite FrameSprite;
        public Sprite CardSprite;
        public Color BackgroundColor;
        public int RhythmDifficulty;
    }
}
