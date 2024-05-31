using DarkChocoSoft.Module;
using Firebase.Auth;
using System;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class FirebaseAuthManager : Singleton<FirebaseAuthManager>
    {
        private const string MANAGER_NAME = "[ FirebaseAuthManager ]";

        private FirebaseAuth m_Auth;
        private FirebaseUser m_User;
        private Action<bool> m_LoginStateAction;

        public void Init()
        {
            if (m_Auth == null)
            {
                m_Auth = FirebaseAuth.DefaultInstance;
                m_Auth.StateChanged += OnChanged;
            }
        }

        public void CreateUser(string email, string password)
        {
            m_Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("회원가입 취소");

                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("회원가입 실패");

                    return;
                }

                AuthResult result = task.Result;

                Debug.Log("회원가입 완료");
            });
        }

        public void Login(string email, string password)
        {
            m_Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("로그인 취소");

                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("로그인 실패");

                    return;
                }

                AuthResult result = task.Result;
            });
        }

        public void Logout()
        {
            m_Auth.SignOut();
            m_Auth.StateChanged -= OnChanged;
            m_Auth = null;
        }

        public void SetLoginStateChangedListener(Action<bool> callback)
        {
            m_LoginStateAction += callback;
        }

        private void OnChanged(object sender, EventArgs eventArgs)
        {
            if (m_Auth.CurrentUser != m_User)
            {
                bool isSigned = m_Auth.CurrentUser != m_User && m_Auth.CurrentUser != null && m_Auth.CurrentUser.IsValid();

                if (!isSigned && m_User != null)
                {
                    Debug.Log("로그아웃");

                    m_LoginStateAction?.Invoke(false);
                }

                m_User = m_Auth.CurrentUser;

                if (isSigned)
                {
                    Debug.Log("로그인");

                    m_LoginStateAction?.Invoke(true);
                }
            }
        }

        public override void Awake()
        {
            base.Awake();

            SetupName(MANAGER_NAME);

            Init();
        }

        private void OnDestroy()
        {
            Logout();
        }
    }
}
