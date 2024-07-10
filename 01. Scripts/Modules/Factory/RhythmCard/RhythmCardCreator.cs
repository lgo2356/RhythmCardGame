using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public abstract class RhythmCardCreator : MonoBehaviour
    {
        protected Vector2 m_Position;
        protected Transform m_Parent;
        protected GameObject m_Prefab;
        protected RhythmCardDto m_Data;

        public abstract void SetPositionAndParent(Vector2 position, Transform parent);
        public abstract void SetPrefab(GameObject prefab);
        public abstract void SetData(RhythmCardDto data);
        protected abstract UI_RhythmCard Create();

        public UI_RhythmCard Get()
        {
            if (m_Position == null)
                throw new System.Exception("Position is not set.");

            if (m_Parent == null)
                throw new System.Exception("Parent is not set.");

            if (m_Prefab == null)
                throw new System.Exception("Prefab is not set.");

            if (m_Data == null)
                throw new System.Exception("Data is not set.");

            UI_RhythmCard card = Create();

            return card;
        }
    }
}
