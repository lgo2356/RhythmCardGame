using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class RhythmPivotObjectPool : Singleton<RhythmPivotObjectPool>
    {
        private const string MANAGER_NAME = "[ RhythmPivotObjectPool ]";

        Queue<RhythmNote> m_Pool = new();

        public GameObject Prefab
        {
            get; set;
        }

        public void Enqueue(RhythmNote obj)
        {
            obj.gameObject.SetActive(false);
            m_Pool.Enqueue(obj);
        }

        public RhythmNote Dequeue()
        {
            if (m_Pool.TryDequeue(out var obj))
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                GameObject clone = ResourceManager.Instance.Instantiate(Prefab, transform);

                if (clone.TryGetComponent<RhythmNote>(out var newObj))
                {
                    newObj.gameObject.SetActive(true);

                    return newObj;
                }
            }

            return obj;
        }

        protected override void Awake()
        {
            base.Awake();

            SetGameObjectName(MANAGER_NAME);
        }
    }
}
