using DarkChocoSoft.RhythmCardGame.Const;
using System.Collections;

namespace DarkChocoSoft.RhythmCardGame.Interface
{
    public interface ISceneLoader
    {
        public void Load(SceneName sceneName);
        public IEnumerator LoadSceneAsyncCoroutine(string sceneName);
    }
}
