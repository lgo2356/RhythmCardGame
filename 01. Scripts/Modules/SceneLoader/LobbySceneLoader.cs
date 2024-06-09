using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Interface;
using DarkChocoSoft.RhythmCardGame.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.Module
{
    public class LobbySceneLoader : Singleton<LobbySceneLoader>, ISceneLoader
    {
        public void Load(SceneName sceneName)
        {
            string name = SceneName.LobbyScene.ToString();

            Addressables.InstantiateAsync(AssetPath.LOADING_SCREEN_PATH).Completed += (obj) =>
            {
                UI_LoadingScreen loadingScreen = obj.Result.GetComponent<UI_LoadingScreen>();
                loadingScreen.SetLoadingImage();

                StartCoroutine(LoadSceneAsyncCoroutine(name));
            };
        }

        public IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
