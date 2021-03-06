﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrostTower : Tower
{
    [SerializeField]
    private float slowingFactor;

    public float SlowingFactor
    {
        get 
        { 
            return slowingFactor; 
        }
    }

    private void Start()
    {
        ElementType = Element.FROST;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(40, 5, 1, 4, 10),
            new TowerUpgrade(80, 10, 1, 8, 20),
        };
    }

    public override Debuff GetDebuff()
    {
        return new FrostDebuff(slowingFactor, DebuffDuration, Target);
    }

    public override string GetStats()
    {
        if (NextUpgrade != null)  //If the next is avaliable
        {
            return string.Format("<color=#00ffffff>{0}</color>{1} \nSlowing factor: {2}% <color=#00ff00ff>+{3}</color>", "<size=20><b>Frost</b></size>", base.GetStats(), SlowingFactor, NextUpgrade.SlowingFactor);
        }

        //Returns the current upgrade
        return string.Format("<color=#00ffffff>{0}</color>{1} \nSlowing factor: {2}%", "<size=20><b>Frost</b></size>", base.GetStats(), SlowingFactor);
    }

    public override void Upgrade()
    {
        this.slowingFactor += NextUpgrade.SlowingFactor;
        base.Upgrade();
    }
}
