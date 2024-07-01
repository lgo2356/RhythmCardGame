using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_BattleSceneRoot : MonoBehaviour
    {
        [SerializeField] private AssetReference RhythmPopupAssetRef;
        [SerializeField] private UI_BattleField m_BattleField;
        [SerializeField] private RectTransform m_HUD;
        [SerializeField] private UI_StatusPanel m_PlayerStatusPanel;
        [SerializeField] private UI_StatusPanel m_MonsterStatusPanel;
        [SerializeField] private Button m_TurnButton;
        [SerializeField] private Button m_CardButton;

        public UI_BattleField BattleField => m_BattleField;
        public UI_StatusPanel PlayerStatusPanel => m_PlayerStatusPanel;
        public UI_StatusPanel MonsterStatusPanel => m_MonsterStatusPanel;
        public Button TurnButton => m_TurnButton;
        public Button CardButton => m_CardButton;

        private void LoadPopup()
        {
            PopupManager.Instance.LoadPopup(RhythmPopupAssetRef, transform);
        }

        private void Awake()
        {
            LoadPopup();
        }
    }
}
