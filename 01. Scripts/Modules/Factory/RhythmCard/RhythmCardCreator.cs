using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public abstract class RhythmCardCreator : MonoBehaviour
    {
        protected Vector2 m_Position;
        protected Transform m_Parent;
        protected string m_ConfigPath;
        protected GameObject m_Prefab;

        public abstract void SetPositionAndParent(Vector2 position, Transform parent);
        public abstract void SetConfig(string configPath);
        public abstract void SetPrefab(GameObject prefab);
        protected abstract UI_RhythmCard Create();

        public UI_RhythmCard Get()
        {
            if (m_Position == null)
                throw new System.Exception("Position is not set.");

            if (m_Parent == null)
                throw new System.Exception("Parent is not set.");

            if (m_ConfigPath == null)
                throw new System.Exception("Config path is not set.");

            if (m_Prefab == null)
                throw new System.Exception("Prefab is not set.");

            UI_RhythmCard card = Create();

            ResourceManager.Instance.LoadAsync<ScriptableObject>(m_ConfigPath, (config) =>
            {
                card.SetConfig(config);
            });

            return card;
        }
    }
}
