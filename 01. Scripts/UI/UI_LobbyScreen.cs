using DarkChocoSoft.RhythmCardGame.Manager;
using Firebase.Firestore;
using System.Collections.Generic;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_LobbyScreen : MonoBehaviour
    {
        private void Start()
        {
            FirebaseFirestoreManager.Instance.Init();

            FirebaseFirestoreManager.Instance.Read("user", (snapshot) =>
            {
                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    Debug.Log(string.Format("User: {0}", doc.Id));

                    Dictionary<string, object> dic = doc.ToDictionary();

                    Debug.Log(string.Format("Name: {0}", dic["name"]));
                }
            });

            Dictionary<string, object> query1 = new()
            {
                { "Name", "전승훈" }
            };

            FirebaseFirestoreManager.Instance.Write("user", "administer", query1, (isSuccess) =>
            {
                if (isSuccess)
                {
                    Debug.Log("데이터 쓰기 성공");
                }
            });

            Dictionary<string, object> query2 = new()
            {
                { "Name", "김우빈" }
            };

            FirebaseFirestoreManager.Instance.Write("user", "administer2", query2, (isSuccess) =>
            {
                if (isSuccess)
                {
                    Debug.Log("데이터 쓰기 성공");
                }
            });
        }
    }
}
