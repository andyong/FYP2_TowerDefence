using UnityEngine;
using System.Collections;

public class EnemyDestructionDelegate : MonoBehaviour {

    public delegate void EnemyDelegate(MoveEnemy enemy);
    public EnemyDelegate enemyDelegate;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        if(enemyDelegate != null)
        {
            enemyDelegate(gameObject.GetComponent<MoveEnemy>());
        }
    }
}
