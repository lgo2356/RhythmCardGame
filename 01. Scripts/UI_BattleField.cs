using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class UI_BattleField : MonoBehaviour
    {
        [SerializeField] private RectTransform m_PlayerPositionTransform;
        [SerializeField] private RectTransform m_MonsterPositionTransform;

        public Transform PlayerPositionTransform
        {
            get
            {
                return m_PlayerPositionTransform;
            }
        }

        public Transform MonsterPositionTransform
        {
            get
            {
                return m_MonsterPositionTransform;
            }
        }
    }
}
