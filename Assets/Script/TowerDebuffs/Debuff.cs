using UnityEngine;
using System.Collections;

public abstract class Debuff {

    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public GameObject target;

    public Debuff(GameObject target)
    {
        this.target = target;
    }

    public virtual void Update()
    {

    }
}
