using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame
{
    public class RhythmNote : MonoBehaviour, IProduct
    {
        public RhythmNoteConfig Config
        { 
            get; 
            private set; 
        }

        public void SetConfig(ScriptableObject config)
        { 
            Config = config as RhythmNoteConfig;
        }

        public void SetSpeed(int value)
        { 
            Config.Speed = value;
        }

        public void StartMove()
        {
            StartCoroutine(MoveCoroutine());
        }

        public void LoadConfig(string path)
        {
            ResourceManager.Instance.LoadAsync<RhythmNoteConfig>(path, (config) =>
            {
                SetConfig(config);
            });
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                if (Config == null)
                {
                    yield return null;
                    continue;
                }

                transform.localPosition += Vector3.right * Config.Speed * Time.deltaTime;

                yield return null;
            }
        }
    }
}
