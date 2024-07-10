using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.Module;
using UnityEngine.EventSystems;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_LobbySceneStartButton : UI_Button
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            CharacterName selectedCharacter = LobbySceneGameManager.Instance.SelectedCharacterType;

            BattleSceneLoader.Instance
                .SetPrefsData(PlayerPrefsKey.PlayerCharacterName, selectedCharacter.ToString())
                .Load(SceneName.BattleScene);

            LobbySceneGameManager.Instance.ReleaseAllReference();
        }
    }
}
