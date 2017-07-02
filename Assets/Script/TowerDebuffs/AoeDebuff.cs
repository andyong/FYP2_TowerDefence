using UnityEngine;
using System.Collections;

public class AoeDebuff : Debuff
{
    //private float slowingFactor;

    //private bool applied;

    public AoeDebuff(MoveEnemy target, float duration)
        : base(target, duration)
    {
        if(target != null)
        {
            target.speed = 0;
        }
        //Debug.Log("AoeDebuff");
    }

    //public override void Update()
    //{

    //    base.Update();
    //}

    public override void Remove()
    {
        if (target != null)
        {
            target.speed = target.MaxSpeed;
            base.Remove();
        }
    }

}
