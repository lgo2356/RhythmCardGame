using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public enum SceneName
    {
        MainScene, LobbyScene, BattleScene,
    }

    public class SceneManager : Singleton<SceneManager>
    {
        private const string MANAGER_NAME = "[ SceneManager ]";
        private const string LOADING_SCREEN_PATH = "Assets/04. Prefabs/UI/UI_LoadingScreen.prefab";

        [SerializeField]
        private AssetReferenceGameObject m_LoadingScreen;

        public void Load(SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.MainScene:
                    {
                        LoadMainScene();
                    }
                    break;

                case SceneName.LobbyScene:
                    {
                        LoadLobbyScene();
                    }
                    break;

                case SceneName.BattleScene:
                    {
                        LoadBattleScene();
                    }
                    break;
            }
        }

        private void LoadMainScene()
        {

        }

        private void LoadLobbyScene()
        {
            string name = SceneName.LobbyScene.ToString();

            Addressables.InstantiateAsync(LOADING_SCREEN_PATH).Completed += (obj) =>
            {
                UI_LoadingScreen loadingScreen = obj.Result.GetComponent<UI_LoadingScreen>();
                loadingScreen.SetLoadingImage();

                StartCoroutine(LoadSceneAsyncCoroutine(name));
            };
        }

        private void LoadBattleScene()
        {
            string name = SceneName.BattleScene.ToString();

            Addressables.InstantiateAsync(LOADING_SCREEN_PATH).Completed += (obj) =>
            {
                UI_LoadingScreen loadingScreen = obj.Result.GetComponent<UI_LoadingScreen>();
                loadingScreen.SetLoadingImage();

                StartCoroutine(LoadSceneAsyncCoroutine(name));
            };
        }

        private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            Destroy(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();

            SetGameObjectName(MANAGER_NAME);
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
        }
    }
}
