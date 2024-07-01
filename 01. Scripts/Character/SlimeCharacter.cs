using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class SlimeCharacter : PlayerCharacter
    {
        public override void Load()
        {
            string path = "Assets/Bolt 2D JellyFarm VE2/Sprites/InGame/Jelly 0.png";

            ResourceManager.Instance.LoadAsync<Sprite>(path, (sprite) =>
            {
                m_CharacterImage.sprite = sprite;
            });
        }

        public void InitStat()
        {
            m_Stat.Init(70, 10);
        }
    }
}
