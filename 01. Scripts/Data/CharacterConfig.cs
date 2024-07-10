using DarkChocoSoft.RhythmCardGame.Const;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Data
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterType CharacterType;
        public CharacterName CharacterName;
        public Sprite CharacterSprite;
        public int MaxHp;
        public int AttackDamage;
    }
}
