using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireTower : Tower {

    private void Start()
    {
        ElementType = Element.FIRE;
    }

    public override Debuff GetDebuff()
    {
        //return new FireDebuff(target);
        return null;
    }
}
