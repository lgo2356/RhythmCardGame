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
    public void Load(CharacterName characterType)
    {
        switch (characterType)
        {
            case CharacterName.Slime:
                {
                    string path = "Assets/Bolt 2D JellyFarm VE2/Sprites/InGame/Jelly 0.png";

                    ResourceManager.Instance.LoadAsync<Sprite>(path, (sprite) =>
                    {
                        m_CharacterImage.sprite = sprite;
                    });
                }
                break;

            case CharacterName.Cat:
                {
                    string path = "Assets/Bolt 2D JellyFarm VE2/Sprites/InGame/Jelly 6.png";

                    ResourceManager.Instance.LoadAsync<Sprite>(path, (sprite) =>
                    {
                        m_CharacterImage.sprite = sprite;
                    });
                }
                break;
        }
    }

    public void LoadConfig(string path)
    {
        ResourceManager.Instance.LoadAsync<CharacterConfig>(path, (config) =>
        {
            ApplyConfig(config);
        });
    }

    private void ApplyConfig(CharacterConfig config)
    {
        CharacterSprite = config.CharacterSprite;
        m_CharacterImage.sprite = CharacterSprite;
    }

    protected virtual void Awake()
    {
        m_CharacterImage = Utils.FindChild<Image>(gameObject);
        m_CharacterImage.sprite = CharacterSprite;
    }
}
