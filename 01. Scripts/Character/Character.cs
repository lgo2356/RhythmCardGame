using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame
{
    public abstract class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected Sprite CharacterSprite;

        protected Image m_CharacterImage;

        public abstract void Load();

        public void SetConfig(ScriptableObject config)
        {
            CharacterSprite = (config as CharacterConfig).CharacterSprite;
            m_CharacterImage.sprite = CharacterSprite;
        }

        public void LoadConfig(string path)
        {
            ResourceManager.Instance.LoadAsync<CharacterConfig>(path, (config) =>
            {
                SetConfig(config);
            });
        }

        public void Attack(Character defender, float damage)
        {
            defender.Defend(this, damage);
        }

        public void Defend(Character attacker, float damage)
        {
            Debug.Log("Damaged : " + damage);
        }

        protected virtual void Awake()
        {
            m_CharacterImage = Utils.FindChild<Image>(gameObject);
            m_CharacterImage.sprite = CharacterSprite;
        }
    }
}
