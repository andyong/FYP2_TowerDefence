using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrostTower : Tower
{

    private void Start()
    {
        ElementType = Element.FROST;
    }

    public override Debuff GetDebuff()
    {
        return new FrostDebuff(target);
        //sreturn null;
    }
}
