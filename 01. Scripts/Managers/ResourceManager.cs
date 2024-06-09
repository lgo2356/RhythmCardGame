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
        private List<GameObject> m_GameObjectPool = new();

        public void InstantiateAsync(string path, Transform parent, Action < GameObject> callback)
        {
            Addressables.InstantiateAsync(path, parent).Completed += (obj) =>
            {
                m_GameObjectPool.Add(obj.Result);

                callback?.Invoke(obj.Result);
            };
        }

        public void InstantiateAsync(AssetReference assetRef, Transform parent, Action<GameObject> callback)
        {
            Addressables.InstantiateAsync(assetRef, parent).Completed += (obj) =>
            {
                m_GameObjectPool.Add(obj.Result);

                callback?.Invoke(obj.Result);
            };
        }

        public GameObject InstantiateSync(string path, Transform parent)
        {
            var handle = Addressables.InstantiateAsync(path, parent);
            handle.WaitForCompletion();

            return handle.Result;
        }

        public void ReleaseGameObject(GameObject go)
        {
            if (m_GameObjectPool.Contains(go))
            {
                Addressables.Release(go);

                m_GameObjectPool.Remove(go);
            }
        }

        public void ReleaseAllGameObject()
        {
            if (m_GameObjectPool.Count <= 0)
                return;

            foreach (var go in m_GameObjectPool)
            {
                Addressables.Release(go);
            }

            m_GameObjectPool.Clear();
        }

        public void LoadAsync<T>(string path, Action<T> callback)
        {
            Addressables.LoadAssetAsync<T>(path).Completed += (obj) =>
            {
                T result = obj.Result;

                callback?.Invoke(result);
            };
        }

        public void LoadAsync<T>(AssetReference assetRef, Action<T> callback)
        {
            if (assetRef == null)
            {
                Debug.LogError("AssetReference is null");
                return;
            }

            Addressables.LoadAssetAsync<T>(assetRef).Completed += (obj) =>
            {
                T result = obj.Result;

                callback?.Invoke(result);
            };
        }

        public T LoadSync<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            handle.WaitForCompletion();

            return handle.Result;
        }

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
