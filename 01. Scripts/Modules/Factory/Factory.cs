using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public abstract class Factory : MonoBehaviour
    {
        public abstract IProduct GetProduct(Vector2 pos, Transform parent);

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            
        }
    }
}
