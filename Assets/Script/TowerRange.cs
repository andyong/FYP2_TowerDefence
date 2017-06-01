using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerRange : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private SpriteRenderer mySpriteRenderer;

    //private SpawnEnemy target;

    

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

    //void OnEnemyDestroy(GameObject enemy)
    //{
    //    enemiesInRange.Remove(enemy);
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    // 2
    //    if (other.gameObject.tag.Equals("Enemy"))
    //    {
    //        enemiesInRange.Add(other.gameObject);
    //        EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
    //        if (del != null)
    //            del.enemyDelegate += OnEnemyDestroy;
    //    }
    //}
    //// 3
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.tag.Equals("Enemy"))
    //    {
    //        enemiesInRange.Remove(other.gameObject);
    //        EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
    //        if (del != null)
    //            del.enemyDelegate -= OnEnemyDestroy;
    //    }
    //}

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
}