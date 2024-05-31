using UnityEngine;

namespace DarkChocoSoft.Module
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if (m_Instance == null)
                    {
                        SetupInstance();
                    }
                }

                return m_Instance;
            }
        }

        public void SetupName(string name)
        {
            gameObject.name = name;
        }

        private static void SetupInstance()
        {
            m_Instance = (T)FindObjectOfType(typeof(T));

            if (m_Instance == null)
            {
                GameObject go = new()
                {
                    name = typeof(T).Name
                };

                m_Instance = go.AddComponent<T>();

                DontDestroyOnLoad(m_Instance);
            }
        }

        private void RemoveDuplicate()
        {
            if (m_Instance == null)
            {
                m_Instance = this as T;

                DontDestroyOnLoad(m_Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public virtual void Awake()
        {
            RemoveDuplicate();
        }
    }
}
