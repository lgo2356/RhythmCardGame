using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : Factory
{
    public override IProduct GetProduct()
    {
        return new PlayerCharacter();
    }
}
