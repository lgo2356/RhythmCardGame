using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DarkChocoSoft.RhythmCardGame.UI
{
    public class UI_LobbyScreen : MonoBehaviour
    {
        public AssetReference CharacterSelectPopupAssetRef;

        public UI_CharacterSelectPopup UI_CharacterSelectPopup;
        public UI_StageSelectPopup UI_StageSelectPopup;
        public UI_CharacterSelectPopupButton UI_CharacterSelectPopupButton;
        public UI_StageSelectPopupButton UI_StageSelectPopupButton;

        private void Init()
        {
            PopupManager.Instance.LoadPopup(CharacterSelectPopupAssetRef, transform);


            UI_CharacterSelectPopupButton.CharacterSelectPopup = UI_CharacterSelectPopup;



            //UI_CharacterSelectPopupButton.StageSelectPopup = UI_StageSelectPopup;
            //UI_StageSelectPopupButton.CharacterSelectPopup = UI_CharacterSelectPopup;
            //UI_StageSelectPopupButton.StageSelectPopup = UI_StageSelectPopup;
        }

        private void Awake()
        {
            Init();
        }

        private void Start()
        {

        }
    }
}

//FirebaseFirestoreManager.Instance.Init();

//FirebaseFirestoreManager.Instance.Read("user", (snapshot) =>
//{
//    foreach (DocumentSnapshot doc in snapshot.Documents)
//    {
//        Debug.Log(string.Format("User: {0}", doc.Id));

//        Dictionary<string, object> dic = doc.ToDictionary();

//        Debug.Log(string.Format("Name: {0}", dic["name"]));
//    }
//});

//Dictionary<string, object> query1 = new()
//            {
//                { "Name", "전승훈" }
//            };

//FirebaseFirestoreManager.Instance.Write("user", "administer", query1, (isSuccess) =>
//{
//    if (isSuccess)
//    {
//        Debug.Log("데이터 쓰기 성공");
//    }
//});

//Dictionary<string, object> query2 = new()
//            {
//                { "Name", "김우빈" }
//            };

//FirebaseFirestoreManager.Instance.Write("user", "administer2", query2, (isSuccess) =>
//{
//    if (isSuccess)
//    {
//        Debug.Log("데이터 쓰기 성공");
//    }
//});
