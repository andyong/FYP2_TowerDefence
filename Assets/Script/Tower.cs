using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Element {AOE, FIRE, FROST, POISON, NONE }

public abstract class Tower : MonoBehaviour {
    public List<GameObject> enemiesInRange;

    [SerializeField]
    public GameObject bullet;

    

    //private Tower towerData;

    private bool canAttack = true;
    private float attackTimer;
    [SerializeField]
    private float attackCooldown;

    public Element ElementType { get; protected set; }

	// Use this for initialization
	void Start () {
        enemiesInRange = new List<GameObject>();
        //towerData = gameObject.GetComponentInChildren<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(target);
        GameObject target = null;
        // 1
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                float distanceToGoal = enemy.GetComponent<MoveEnemy>().distanceToGoal();
                if (distanceToGoal < minimalEnemyDistance)
                {
                    target = enemy;
                    minimalEnemyDistance = distanceToGoal;
                }
            }

        }
        // 2
        if (target != null)
        {
            if (!canAttack)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= attackCooldown)
                {
                    canAttack = true;
                    attackTimer = 0;
                }
            }
            if (canAttack)
            {
                Shoot(target.GetComponent<Collider2D>());

                canAttack = false;
            }
            //// 3 //DON'T NEED TO ROTATE TOWERS
            //Vector3 direction = gameObject.transform.position - target.transform.position;
            //gameObject.transform.rotation = Quaternion.AngleAxis(
            //    Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
            //    new Vector3(0, 0, 1));
        }
	}

    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 2
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            if (del != null)
                del.enemyDelegate += OnEnemyDestroy;
        }
    }
    // 3
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            if (del != null)
                del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    public abstract Debuff GetDebuff();

    void Shoot(Collider2D target)
    {
        Vector3 offset = new Vector3(0.70f, -0.70f, 0.0f);
        GameObject bulletPrefab = bullet;
        // 1 
        Vector3 startPosition = gameObject.transform.position + offset;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehaviour bulletComp = newBullet.GetComponent<BulletBehaviour>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        if (bulletPrefab.transform.position == target.transform.position)
        {
            ReleaseObject(bullet);
        }

        // 3 
        //Animator animator =
        //    monsterData.CurrentLevel.visualization.GetComponent<Animator>();
        //animator.SetTrigger("fireShot");
        //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.PlayOneShot(audioSource.clip);
    }

    public void ReleaseObject(GameObject gameobject)
    {
        gameobject.SetActive(false);
    }
}
