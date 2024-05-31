using UnityEngine.EventSystems;
using UnityEngine;
using DarkChocoSoft.RhythmCardGame.Manager;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_PressToStartButton : UI_Button, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");

            SceneManager.Instance.Load(SceneName.LobbyScene);
        }
    }
}
