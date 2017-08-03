using UnityEngine;
using System.Collections;

public class PoisonDebuff : Debuff
{
    private float timeSinceTick;

    private float tickTime;

    private PoisonSplash splashPrefab;

    private int splashDamage;

    public PoisonDebuff(int splashDamage, float tickTime, PoisonSplash splashPrefab, float duration, MoveEnemy target)
        : base(target,duration)
    {
        Debug.Log("PoisonDebuff");
        this.splashDamage = splashDamage;
        this.tickTime = tickTime;
        this.splashPrefab = splashPrefab; 
    }

    public override void Update()
    {
        if (target != null)
        {
            timeSinceTick += Time.deltaTime;

            if (timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                Splash();
            }
        }

        base.Update();
    }

    private void Splash()
    {
        SoundManager.Instance.PlaySFX("splat");

        PoisonSplash tmp = (PoisonSplash)GameObject.Instantiate(splashPrefab, target.transform.position, Quaternion.identity);

        tmp.Damage = splashDamage;

        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), tmp.GetComponent<Collider2D>());
    }
}
