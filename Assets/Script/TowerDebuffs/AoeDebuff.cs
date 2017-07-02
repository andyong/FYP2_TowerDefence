using UnityEngine;
using System.Collections;

public class AoeDebuff : Debuff
{
    public AoeDebuff(MoveEnemy target)
        : base(target,1)
    {
        Debug.Log("AoeDebuff");
    }

    public override void Update()
    {

        base.Update();
    }

}
