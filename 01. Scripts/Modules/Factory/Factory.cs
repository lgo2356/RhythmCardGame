using DarkChocoSoft.Module;
using System.Collections;
using System.Collections.Generic;
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
