using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerRange : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private SpriteRenderer mySpriteRenderer;

    //private SpawnEnemy target;
    public int Price { get; set; }
    

    // Use this for initialization
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(target);
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
}