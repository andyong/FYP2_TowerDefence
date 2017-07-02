using UnityEngine;
using System.Collections;

public class FrostDebuff : Debuff
{

    public FrostDebuff(MoveEnemy target)
        : base(target,1)
    {
        Debug.Log("FrostDebuff");
    }

    public override void Update()
    {

        base.Update();
    }
}
