using DarkChocoSoft.Module;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class FirebaseFirestoreManager : Singleton<FirebaseFirestoreManager>
    {
        private const string MANAGER_NAME = "[ FirebaseFirestoreManager ]";

        private FirebaseFirestore m_DB;

        public void Init()
        {
            if (m_DB == null)
            {
                m_DB = FirebaseFirestore.DefaultInstance;
            }
        }

        public void Read(string path, Action<QuerySnapshot> callback)
        {
            CollectionReference userRef = m_DB.Collection(path);

            userRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                QuerySnapshot snapshot = task.Result;
                
                callback.Invoke(snapshot);
            });
        }

        public void Write(string collectionPath, string documentPath, Dictionary<string, object> query, Action<bool> callback)
        {
            DocumentReference documentRef = m_DB.Collection(collectionPath).Document(documentPath);

            documentRef.SetAsync(query).ContinueWithOnMainThread(task =>
            {
                callback.Invoke(true);
            });
        }

        protected override void Awake()
        {
            base.Awake();

            SetupName(MANAGER_NAME);

            Init();
        }

        private void Start()
        {

        }

        private void OnDestroy()
        {
            m_DB = null;
        }
    }
}
