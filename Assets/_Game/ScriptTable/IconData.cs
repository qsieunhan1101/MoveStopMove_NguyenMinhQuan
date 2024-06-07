using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Icon Data", menuName = "IconData")]
public class IconData : ScriptableObject
{
    [SerializeField] private List<Sprite> iconHatSprites;
    [SerializeField] private List<Sprite> iconPantSprites;
    [SerializeField] private List<Sprite> iconShieldSprites;
    [SerializeField] private List<Sprite> iconSkinSprites;

    [SerializeField] private List <Sprite> iconWeaponSprites;
    [SerializeField] private List <String> nameWeapons;


    public Sprite GetIconHatSprite(IconIndex indexIcon)
    {
        if ((int)indexIcon > iconHatSprites.Count-1)
        {
            return iconHatSprites[(int)indexIcon % iconHatSprites.Count];
        }
        return iconHatSprites[(int)indexIcon];
    }

    public Sprite GetIconPantSprite(IconIndex indexIcon)
    {
        if ((int)indexIcon > iconPantSprites.Count - 1)
        {
            return iconPantSprites[(int)indexIcon % iconPantSprites.Count];
        }
        return iconPantSprites[(int)indexIcon];
    }

    public Sprite GetIconShieldSprite(IconIndex indexIcon)
    {
        if ((int)indexIcon > iconShieldSprites.Count - 1)
        {
            return iconShieldSprites[(int)indexIcon % iconShieldSprites.Count];
        }
        return iconShieldSprites[(int)indexIcon];
    }

    public Sprite GetIconSkinSprite(IconIndex indexIcon)
    {
        if ((int)indexIcon > iconSkinSprites.Count - 1)
        {
            return iconSkinSprites[(int)indexIcon % iconSkinSprites.Count];
        }
        return iconSkinSprites[(int)indexIcon];
    }
    ///

    public Sprite GetIconWeaponSprite(IconIndex indexIcon)
    {
        if ((int)indexIcon > iconWeaponSprites.Count - 1)
        {
            return iconWeaponSprites[iconWeaponSprites.Count-1];
        }
        return iconWeaponSprites[(int)indexIcon];
    }


    public String GetNameWeapon(IconIndex indexIcon)
    {
        if ((int)indexIcon > nameWeapons.Count - 1)
        {
            return nameWeapons[nameWeapons.Count-1];
        }
        return nameWeapons[(int)indexIcon];
    }

}
