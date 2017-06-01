using UnityEngine;
using System.Collections;

public class FireDebuff : Debuff {

    BulletBehaviour bullet;
    public int damage = 1;
    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public FireDebuff(GameObject target) : base(target)
    {

    }

    public override void Update()
    {
        //Transform healthBarTransform = target.transform.FindChild("HealthBar");
        //HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
        //healthBar.currentHealth -= Mathf.Max(damage, 0);
        
        base.Update();
    }
}
