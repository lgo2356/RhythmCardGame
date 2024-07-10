using DarkChocoSoft.RhythmCardGame.UI;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class DefaultRhythmCardCreator : RhythmCardCreator
    {
        public override void SetPositionAndParent(Vector2 position, Transform parent)
        {
            m_Position = position;
            m_Parent = parent;
        }

        public override void SetConfig(string configPath)
        {
            m_ConfigPath = configPath;
        }

        public override void SetPrefab(GameObject prefab)
        {
            m_Prefab = prefab;
        }

        protected override UI_RhythmCard Create()
        {
            GameObject instance = Instantiate(m_Prefab, m_Position, Quaternion.identity, m_Parent);
            UI_SingleRhythmCard result = instance.GetOrAddComponent<UI_SingleRhythmCard>();

            return result;
        }
    }
}
