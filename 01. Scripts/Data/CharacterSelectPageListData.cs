using System.Collections.Generic;

namespace DarkChocoSoft.RhythmCardGame.Data
{
    [System.Serializable]
    public class CharacterSelectPageListData
    {
        public List<CharacterSelectPageData> pages;
    }

    [System.Serializable]
    public class CharacterSelectPageData
    {
        public string type;
        public string name_en;
        public string name_kr;
        public string img_path;
    }
}
