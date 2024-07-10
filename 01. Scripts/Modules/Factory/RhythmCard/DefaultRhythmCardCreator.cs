using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
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

        public override void SetPrefab(GameObject prefab)
        {
            m_Prefab = prefab;
        }

        public override void SetData(RhythmCardDto data)
        {
            m_Data = data;
        }

        protected override UI_RhythmCard Create()
        {
            GameObject instance = Instantiate(m_Prefab, m_Position, Quaternion.identity, m_Parent);
            
            UI_SingleRhythmCard result = instance.GetOrAddComponent<UI_SingleRhythmCard>();
            result.SetData(m_Data);

            ResourceManager.Instance.LoadAsync<ScriptableObject>(m_Data.config_path, (config) =>
            {
                result.SetConfig(config);
            });

            return result;
        }
    }
}
