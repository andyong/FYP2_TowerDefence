﻿using UnityEngine;
using System.Collections;

public class FrostDebuff : Debuff
{

    public FrostDebuff(MoveEnemy target)
        : base(target)
    {
        Debug.Log("FrostDebuff");
    }

    public override void Update()
    {

        base.Update();
    }
}
