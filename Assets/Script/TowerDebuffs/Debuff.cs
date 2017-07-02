using UnityEngine;
using System.Collections;

public abstract class Debuff
{
    protected MoveEnemy target;

    private float duration;

    private float elapsed;

    public Debuff(MoveEnemy target, float duration)
    {
        this.target = target;
        this.duration = duration; 

    }

    public virtual void Update()
    {
        elapsed += Time.deltaTime;

        if(elapsed >= duration)
        {
            Remove();
        }
    }

    public virtual void Remove()
    {
        if (target != null)
        {
            target.RemoveDebuff(this);
        }
    }
}
