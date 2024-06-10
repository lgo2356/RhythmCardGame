using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Data;
using DarkChocoSoft.RhythmCardGame.Manager;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour, IProduct
{
    [SerializeField] protected Sprite CharacterSprite;

    protected Image m_CharacterImage;

    public abstract void Load();

    public void SetConfig(ScriptableObject config)
    {
        CharacterSprite = (config as CharacterConfig).CharacterSprite;
        m_CharacterImage.sprite = CharacterSprite;
    }

    public void LoadConfig(string path)
    {
        ResourceManager.Instance.LoadAsync<CharacterConfig>(path, (config) =>
        {
            SetConfig(config);
        });
    }

    protected virtual void Awake()
    {
        m_CharacterImage = Utils.FindChild<Image>(gameObject);
        m_CharacterImage.sprite = CharacterSprite;
    }
}
