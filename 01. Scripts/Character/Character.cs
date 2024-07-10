using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame
{
    public interface IAttackable
    {
        public void Attack(Character defender, int damage);
    }

    public interface IDefenceable
    {
        public void Defend(Character attacker, int damage);
    }

    public class Character : MonoBehaviour, IAttackable, IDefenceable
    {
        [SerializeField] protected Sprite CharacterSprite;

        protected Image m_CharacterImage;
        protected Animator m_Anim;
        protected Stat m_Stat;
        protected UI_StatusPanel m_StatusPanel;

        public Stat Stat => m_Stat;

        public virtual void Load()
        {

        }

        public void SetConfig(ScriptableObject config)
        {
            CharacterConfig characterConfig = config as CharacterConfig;

            CharacterSprite = characterConfig.CharacterSprite;
            m_CharacterImage.sprite = CharacterSprite;

            m_Stat.Init(characterConfig.MaxHp, characterConfig.AttackDamage);
        }

        public void LoadConfig(string path)
        {
            CharacterConfig config = ResourceManager.Instance.LoadSync<CharacterConfig>(path);
            SetConfig(config);
        }

        public void ConnectStatusPanel(UI_StatusPanel statusPanel)
        {
            m_StatusPanel = statusPanel;
            m_StatusPanel.InitHp(m_Stat.MaxHp);
        }

        public void Attack(Character defender, int damage)
        {
            Debug.Log("Attack : " + damage);

            StartCoroutine(AttackCoroutine(defender, damage));
        }

        public void Defend(Character attacker, int damage)
        {
            Debug.Log("Damaged : " + damage);

            int currentHp = m_Stat.CurrentHp - damage;

            m_Stat.SetCurrentHp(currentHp);
            m_StatusPanel.SetCurrentHp(currentHp);

            m_Anim.SetTrigger("doTouch");
        }

        private IEnumerator AttackCoroutine(Character defender, int damage)
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
