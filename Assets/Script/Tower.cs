using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Element {AOE, FIRE, FROST, POISON, NONE }

public abstract class Tower : MonoBehaviour {
    public List<MoveEnemy> enemiesInRange;
    private SpriteRenderer mySpriteRenderer;

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

    public float Proc
    {
        get { return proc; }
    }

    [SerializeField]
    private int damage;

    public int Damage
    {
        get { return damage; }
        set { this.damage=value;}
    }

    [SerializeField]
    private int speed;

    public int Speed
    {
        get { return speed; }
    }

    public int Level
    {
        get;
        protected set;
    }

    public int Price { get; set; }

    //private Tower towerData;

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;

    public float AttackCooldown
    {
        get {return attackCooldown ;}
        set {this.attackCooldown = value ;}
    }

    [SerializeField]
    private GameObject levelText;

    public Element ElementType { get; protected set; }

    public TowerUpgrade[] Upgrades { get; protected set; }

	// Use this for initialization
	void Start () {
        enemiesInRange = new List<MoveEnemy>();
       
        //towerData = gameObject.GetComponentInChildren<Tower>();
	}

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        Level = 1;
    }

    public TowerUpgrade NextUpgrade
    {
        get
        {
            if(Upgrades.Length > Level -1)
            {
                return Upgrades[Level - 1];
            }

            return null; 
        }
    }
  
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(target);
        ShowLevel();
        Attack();
	}

    public void Select()
    {
       mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
       GameManager.Instance.UpdateUpgradeTip();
    }

    public void ShowLevel()
    {
        if(Level ==1 )
        {
            levelText.GetComponent<SpriteRenderer>().sprite = Resources.Load("tower_level_1", typeof(Sprite)) as Sprite;
        }
        else if (Level == 2)
        {
            levelText.GetComponent<SpriteRenderer>().sprite = Resources.Load("tower_level_2", typeof(Sprite)) as Sprite;
        }
        else if (Level == 3)
        {
            levelText.GetComponent<SpriteRenderer>().sprite = Resources.Load("tower_level_3", typeof(Sprite)) as Sprite;
        }
    }


    private void Attack()
    {
       target = null;

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
            else
                Debug.Log("NULL");

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
        Vector3 startPosition = gameObject.transform.position ;
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

    public virtual void Upgrade()
    {
        GameManager.Instance.Currency -= NextUpgrade.Price;
        Price += NextUpgrade.Price;
        this.damage += NextUpgrade.Damage;
        this.proc += NextUpgrade.ProcChance;
        this.DebuffDuration += NextUpgrade.DebuffDuration;
        Level++;
        GameManager.Instance.UpdateUpgradeTip();
    }

    public void ReleaseObject(GameObject gameobject)
    {
        gameobject.SetActive(false);
    }

    public virtual string GetStats()
    {

        if(NextUpgrade!=null)
        {
            return string.Format("\nLevel: {0} \nDamage: {1} <color=#00ff00ff> +{4}</color>\nProc: {2}% <color=#00ff00ff> +{5}%</color>\nDebuff: {3}sec <color=#00ff00ff> + {6}</color>",
                Level, damage, proc, DebuffDuration,NextUpgrade.Damage,NextUpgrade.ProcChance,NextUpgrade.DebuffDuration);
        } 
        return string.Format("\nLevel: {0} \nDamage: {1} \nProc {2}% \nDebuff: {3}sec", Level, Damage, Proc, DebuffDuration);
    }

   
}
