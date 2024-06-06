using DarkChocoSoft.RhythmCardGame.Const;
using DarkChocoSoft.RhythmCardGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour, IProduct
{
    [SerializeField] protected Sprite CharacterSprite;

    private Image m_CharacterImage;

    public void Load(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Slime:
                {
                    string path = "Assets/Bolt 2D JellyFarm VE2/Sprites/InGame/Jelly 0.png";

                    ResourceManager.Instance.LoadAsync<Sprite>(path, (sprite) =>
                    {
                        m_CharacterImage.sprite = sprite;
                    });
                }
                break;

            case CharacterType.Cat:
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

    protected virtual void Awake()
    {
        m_CharacterImage = Utils.FindChild<Image>(gameObject);
        m_CharacterImage.sprite = CharacterSprite;
    }
}
