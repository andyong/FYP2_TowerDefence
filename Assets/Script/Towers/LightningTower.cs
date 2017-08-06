using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningTower : Tower
{

    private void Start()
    {
        ElementType = Element.AOE;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(100, 5, 0.5f, 5),
            new TowerUpgrade(180, 10, 1.0f, 10),
        };
    }

    public override Debuff GetDebuff()
    {
        return new LightningDebuff(target, DebuffDuration);
        //return null;
    }

    public override string GetStats()
    {
        return string.Format("<color=#add8e6ff>{0}</color>{1}", "<Size=20><b>Lightning Tower</b></size>", base.GetStats());
    }

}
