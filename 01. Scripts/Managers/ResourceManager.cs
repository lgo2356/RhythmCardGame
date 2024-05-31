using DarkChocoSoft.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        private Stack<GameObject> m_GameObjectPool = new();

        //public GameObject InstantiateAsync(string path)
        //{
        //    Addressables.InstantiateAsync<GameObject>(path).Completed += (obj) =>
        //    {
        //        m_GameObjectPool.Push(obj.Result);

        //        return obj.Result;
        //    };
        //}

        //public void LoadSpriteAsync(string path)
        //{
        //    Addressables.LoadAssetAsync<Sprite>(path).Completed += (obj) =>
        //    {

        //    };
        //}

        private IEnumerator InitAddressableCoroutine()
        {
            var init = Addressables.InitializeAsync();

            yield return init;
        }

        private void Start()
        {
            StartCoroutine(InitAddressableCoroutine());
        }
    }
}
