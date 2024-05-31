using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image m_LoadingImage;

        public void SetLoadingImage()
        {
            Addressables.LoadAssetAsync<Sprite>("Assets/03. Art/darkchocologo.png").Completed += (img) =>
            {
                m_LoadingImage.sprite = img.Result;
            };
        }

        private void Start()
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 99;
        }
    }
}
