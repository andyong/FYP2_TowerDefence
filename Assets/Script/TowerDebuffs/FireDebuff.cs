using UnityEngine;
using System.Collections;

public class FireDebuff : Debuff
{
    //BulletBehaviour bullet;
    //public int damage = 1;
    private float tickTime;

    private float timeSinceTick;

    private float tickDamage;

    public FireDebuff(float tickDamage, float tickTime, float duration, MoveEnemy target)
        : base(target, duration)
    {
        this.tickDamage = tickDamage;
        this.tickTime = tickTime;
    }

    public override void Update()
    {
        if(target!=null)
        {
            timeSinceTick += Time.deltaTime;

            if(timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                target.TakeDamage(tickDamage);
            }
        }

        base.Update();
    }


}
