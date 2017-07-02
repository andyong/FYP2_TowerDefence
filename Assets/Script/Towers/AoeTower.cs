using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AoeTower : Tower
{

    private void Start()
    {
        ElementType = Element.AOE;
    }

    public override Debuff GetDebuff()
    {
        return new AoeDebuff(target, DebuffDuration);
        //return null;
    }
}
