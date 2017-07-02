using UnityEngine;
using System.Collections;

public class AoeDebuff : Debuff
{
    public AoeDebuff(MoveEnemy target)
        : base(target)
    {
        Debug.Log("AoeDebuff");
    }

    public override void Update()
    {

        base.Update();
    }

}
