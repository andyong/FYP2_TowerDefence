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
            if(!applied)
            {
                applied = true;
                target.Speed -= (target.MaxSpeed * slowingFactor) / 100;
            }
        }
        base.Update();
    }

    public override void Remove()
    {
        target.Speed = target.MaxSpeed;
        base.Remove();
    }
}
