using UnityEngine;
using System.Collections;

public class FrostDebuff : Debuff
{
    private float slowingFactor;
    private bool applied;

    public FrostDebuff(float slowingFactor, float duration, MoveEnemy target)
        : base(target, duration)
    {
        Debug.Log("FrostDebuff");
        this.slowingFactor = slowingFactor;
    }

    public override void Update()
    {
        if(target != null)
        {
            if(!applied )
            {
                applied = true;
                if (target.Speed > 0)
                    target.Speed -= (target.MaxSpeed * slowingFactor) / 100;
                else
                    target.Speed = 0;
            }
        }
        base.Update();
    }

    public override void Remove()
    {
        if (target != null)
        {
            target.Speed = target.MaxSpeed;
            base.Remove();
        }
    }
}
