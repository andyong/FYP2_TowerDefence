using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningTower : Tower
{

    private void Start()
    {
        ElementType = Element.AOE;
    }

    public override Debuff GetDebuff()
    {
        return new LightningDebuff(target, DebuffDuration);
        //return null;
    }
}
