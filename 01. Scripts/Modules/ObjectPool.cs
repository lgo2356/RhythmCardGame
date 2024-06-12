using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : Component
    {        
        Queue<T> m_Pool = new();

        public GameObject Prefab
        { 
            get; set; 
        }

        public void Enqueue(T obj)
        {
            obj.gameObject.SetActive(false);
            m_Pool.Enqueue(obj);
        }

        public T Dequeue()
        {
            if (m_Pool.TryDequeue(out var obj))
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                GameObject clone = ResourceManager.Instance.Instantiate(Prefab);

                if (clone.TryGetComponent<T>(out var newObj))
                {
                    newObj.gameObject.SetActive(true);

                    return newObj;
                }
            }

            return obj;
        }
    }
}
