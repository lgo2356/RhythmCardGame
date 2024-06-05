using DarkChocoSoft.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        private Stack<GameObject> m_GameObjectPool = new();

        public void InstantiateAsync(string path, Transform parent, Action < GameObject> callback)
        {
            Addressables.InstantiateAsync(path, parent).Completed += (obj) =>
            {
                m_GameObjectPool.Push(obj.Result);

                callback?.Invoke(obj.Result);
            };
        }

        public void InstantiateAsync(AssetReference assetRef, Transform parent, Action<GameObject> callback)
        {
            Addressables.InstantiateAsync(assetRef, parent).Completed += (obj) =>
            {
                m_GameObjectPool.Push(obj.Result);

                callback?.Invoke(obj.Result);
            };
        }

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
