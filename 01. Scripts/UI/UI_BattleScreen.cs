using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_BattleScreen : MonoBehaviour
    {
        [SerializeField] private AssetReference RhythmPopupAssetRef;

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
