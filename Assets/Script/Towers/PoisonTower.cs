using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonTower : Tower
{

    private void Start()
    {
        ElementType = Element.POISON;
    }

    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(target);
        //return null;
    }
}
