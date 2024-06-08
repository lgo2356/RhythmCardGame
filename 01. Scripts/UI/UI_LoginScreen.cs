using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Manager;
using DarkChocoSoft.RhythmCardGame.Module;
using TMPro;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_LoginScreen : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_EmailInputField;
        [SerializeField] private TMP_InputField m_PasswordInputField;

        public void OnLoginButtonClick()
        {
            FirebaseAuthManager.Instance.Login(m_EmailInputField.text, m_PasswordInputField.text);
        }

        public void OnLogoutButtonClick()
        {
            FirebaseAuthManager.Instance.Logout();
        }

        public void OnCreateUserButtonClick()
        {
            FirebaseAuthManager.Instance.CreateUser(m_EmailInputField.text, m_PasswordInputField.text);
        }

        private void OnLoginStateChanged(bool isLogin)
        {
            if (isLogin)
            {
                LobbySceneLoader.Instance.Load(SceneName.LobbyScene);
            }
        }

        private void Start()
        {
            FirebaseAuthManager.Instance.Init();
            FirebaseAuthManager.Instance.SetLoginStateChangedListener(OnLoginStateChanged);
        }
    }
}
