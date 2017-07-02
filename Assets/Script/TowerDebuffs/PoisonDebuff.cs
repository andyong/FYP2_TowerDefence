using UnityEngine;
using System.Collections;

public class PoisonDebuff : Debuff
{

    public PoisonDebuff(MoveEnemy target)
        : base(target, 1)
    {
        Debug.Log("PoisonDebuff");
    }

    public override void Update()
    {

        base.Update();
    }
}
