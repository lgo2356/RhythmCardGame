using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class DataHolder<T> : MonoBehaviour
    {
        public T Data { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            gameObject.name = $"[ DataHolder ]";
        }
    }
}
