using UnityEngine;
using System.Collections;

public abstract class Debuff
{
    protected MoveEnemy target;

    public Debuff(MoveEnemy target)
    {
        this.target = target;

    }

    public virtual void Update()
    {

    }
}
