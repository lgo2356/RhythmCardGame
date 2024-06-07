using DarkChocoSoft.Module;
using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Module;
using DarkChocoSoft.RhythmCardGame.UI;
using UnityEditor;
using UnityEngine;

namespace DarkChocoSoft.RhythmCardGame.Manager
{
    public class BattleSceneGameManager : Singleton<BattleSceneGameManager>
    {
        private const string MANAGER_NAME = "[ BattleSceneGameManager ]";

        public int StageNumber;
        public CharacterType SelectedCharacterType;

        private GameObject m_CardPanel;
        private Factory[] m_CardFactories = new Factory[4];

        public void GetCard(int cardTypeIndex)
        {
            IProduct product = m_CardFactories[cardTypeIndex].GetProduct(Vector2.zero, m_CardPanel.transform);
        }

        private void InitCardFactory()
        {
            m_CardFactories[0] = Utils.GetOrAddComponent<SingleRhythmCardFactory>(gameObject);
            m_CardFactories[1] = Utils.GetOrAddComponent<DoubleRhythmCardFactory>(gameObject);
            m_CardFactories[2] = Utils.GetOrAddComponent<TripleRhythmCardFactory>(gameObject);
            m_CardFactories[3] = Utils.GetOrAddComponent<LongRhythmCardFactory>(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();

            SetupName(MANAGER_NAME);
            InitCardFactory();

            m_CardPanel = GameObject.Find("CardPanel");
        }

        protected override void Start()
        {
            RemoveDontDestroyOnLoad();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BattleSceneGameManager))]
    public class BattleSceneGameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BattleSceneGameManager manager = target as BattleSceneGameManager;

            if (GUILayout.Button("Get Card 1"))
            {
                manager.GetCard(0);
            }

            if (GUILayout.Button("Get Card 2"))
            {
                manager.GetCard(1);
            }

            if (GUILayout.Button("Get Card 3"))
            {
                manager.GetCard(2);
            }

            if (GUILayout.Button("Get Card 4"))
            {
                manager.GetCard(3);
            }
        }
    }
#endif
}
