using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_BattleScreen : MonoBehaviour
    {
        [SerializeField] private AssetReference RhythmPopupAssetRef;
        [SerializeField] private Button m_TurnButton;
        [SerializeField] private Button m_CardButton;

        public Button TurnButton => m_TurnButton;
        public Button CardButton => m_CardButton;

        public void ShowRhythmPopup()
        {
            PopupManager.Instance.ShowPopup(PopupType.UI_RhythmPopup);
        }

        private void Awake()
        {
            PopupManager.Instance.LoadPopup(RhythmPopupAssetRef, transform);
        }
    }
}
