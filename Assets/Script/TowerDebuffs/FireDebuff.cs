﻿using UnityEngine;
using System.Collections;

public class FireDebuff : Debuff
{
    //BulletBehaviour bullet;
    //public int damage = 1;

    public FireDebuff(MoveEnemy target)
        : base(target, 1)
    {
        Debug.Log("FireDebuff");
    }

    public override void Update()
    {

        base.Update();
    }
}
