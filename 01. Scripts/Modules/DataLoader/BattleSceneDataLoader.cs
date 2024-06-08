using DarkChocoSoft.RhythmCardGame.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class BattleSceneDataLoader : DataLoader
    {
        private Stack<string> m_Paths = new();
        private Action<GameObject> m_OnCompleteAction;

        public BattleSceneDataLoader Add(string path)
        {
            m_Paths.Push(path);
            return this;
        }

        public BattleSceneDataLoader OnComplete(Action<GameObject> callback)
        {
            m_OnCompleteAction = callback;
            return this;
        }

        public void Load()
        {
            while (m_Paths.TryPop(out string path))
            {
                ResourceManager.Instance.LoadAsync<GameObject>(path, (obj) =>
                {
                    m_OnCompleteAction?.Invoke(obj);
                });
            }
        }
    }    
}
