using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Element {AOE, FIRE, FROST, POISON, NONE }

public abstract class Tower : MonoBehaviour {
    public List<MoveEnemy> enemiesInRange;

    [SerializeField]
    public GameObject bullet;

    protected MoveEnemy target;

    public MoveEnemy Target
    {
        get { return target; }
    }

    [SerializeField]
    private float debuffDuration;

    public float DebuffDuration
    {
        get { return debuffDuration; }
        set { this.debuffDuration = value; }
    }

    [SerializeField]
    private float proc;

    [SerializeField]
    private int damage;

    public int Damage
    {
        get { return damage; }
    }

    [SerializeField]
    private int speed;

    public int Speed
    {
        get { return speed; }
    }

    //private Tower towerData;

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;

    public Element ElementType { get; protected set; }

	// Use this for initialization
	void Start () {
        enemiesInRange = new List<MoveEnemy>();
        //towerData = gameObject.GetComponentInChildren<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(target);

        Attack();
	}

    private void Attack()
    {
        // 1
        float minimalEnemyDistance = float.MaxValue;

        foreach (MoveEnemy enemy in enemiesInRange)
        {
            // if enemy in range, make it to tower's target
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

    void OnEnemyDestroy(MoveEnemy enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 2
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject.GetComponent<MoveEnemy>());
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
            enemiesInRange.Remove(other.gameObject.GetComponent<MoveEnemy>());
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
        bulletComp.target = target.gameObject.GetComponent<MoveEnemy>();
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        bulletComp.Initialize(this);

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
