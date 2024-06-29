using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame
{
    public interface IAttackable
    {
        public void Attack(Character defender, float damage);
    }

    public interface IDefenceable
    {
        public void Defend(Character attacker, float damage);
    }

    public abstract class Character : MonoBehaviour, IAttackable, IDefenceable
    {
        [SerializeField] protected Sprite CharacterSprite;

        protected Image m_CharacterImage;
        protected Animator m_Anim;
        protected Stat m_Stat;

        public Stat Stat => m_Stat;

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
            Debug.Log("Attack : " + damage);

            StartCoroutine(AttackCoroutine(defender, damage));
        }

        public void Defend(Character attacker, float damage)
        {
            Debug.Log("Damaged : " + damage);

            m_Anim.SetTrigger("doTouch");
        }

        private IEnumerator AttackCoroutine(Character defender, float damage)
        {
            m_Anim.SetTrigger("doTouch");

            yield return new WaitForSeconds(0.5f);

            defender.Defend(this, damage);
        }
        

        protected virtual void Awake()
        {
            m_CharacterImage = Utils.FindChild<Image>(gameObject);
            m_CharacterImage.sprite = CharacterSprite;

            m_Anim = gameObject.GetComponent<Animator>();
            m_Stat = gameObject.GetOrAddComponent<Stat>();
        }
    }
}
