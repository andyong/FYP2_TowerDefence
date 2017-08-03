using UnityEngine;
using System.Collections;

public class LightningDebuff : Debuff
{

    public LightningDebuff(MoveEnemy target, float duration)
        : base(target, duration)
    {
        if(target != null)
        {
            SoundManager.Instance.PlaySFX("shock");
            target.Speed = 0;
        }
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
