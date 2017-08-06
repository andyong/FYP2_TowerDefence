using UnityEngine;
using System.Collections;

public abstract class Debuff
{
    protected MoveEnemy target;

    protected float duration;

    public float Duration_
    {
        get { return duration; }
        set { this.duration = value; }
    }

    private float elapsed;

    public Debuff(MoveEnemy target, float duration)
    {
        this.target = target;

        this.duration = duration;

    }

    public virtual void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
           
            Remove();
        }

    }

    public virtual void Remove()
    {

        if(target != null)
        {
            target.RemoveDebuff(this);
        }
    }
}
