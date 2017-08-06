using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireTower : Tower
{

    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get { return tickTime; }
    }

    public float TickDamage
    {
        get { return tickDamage; }
    }

    private void Start()
    {
        ElementType = Element.FIRE;
    
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(75, 10, .5f, 10, -0.1f, 1),
            new TowerUpgrade(125, 15, .5f, 15, -0.1f, 1),
        };
    }


    public override Debuff GetDebuff()
    {
        return new FireDebuff(TickDamage, TickTime, DebuffDuration, Target);
        //return null;
    }

    public override string GetStats()
    {
        if (NextUpgrade != null) //If the next is avaliable
        {
            return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2} <color=#00ff00ff>{4}</color>\nTick damage: {3} <color=#00ff00ff>+{5}</color>", "<size=20><b>Fire</b></size> ", base.GetStats(), TickTime, TickDamage, NextUpgrade.TickTime, NextUpgrade.SpecialDamage);
        }

        //Returns the current upgrade
        return string.Format("<color=#ffa500ff>{0}</color>{1} \nTick time: {2}\nTick damage: {3}", "<size=20><b>Fire</b></size> ", base.GetStats(), TickTime, TickDamage);
    }

    public override void Upgrade()
    {
        this.tickTime -= NextUpgrade.TickTime;
        this.tickDamage -= NextUpgrade.SpecialDamage;
        base.Upgrade();
    }
}
