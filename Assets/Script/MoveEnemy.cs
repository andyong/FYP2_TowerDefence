using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveEnemy : MonoBehaviour {

    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;

    [SerializeField]
    private float speed;

    
    [SerializeField]
    private float minimalDist;

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private GameObject frost;

    [SerializeField]
    private GameObject poison;

    [SerializeField]
    private GameObject stun;

    public float Speed
    {
        get { return speed; }
        set { this.speed = value; }
    }

    private List<Debuff> debuffs = new List<Debuff>();

    private List<Debuff> debuffsToRemove = new List<Debuff>();

    private List<Debuff> newDebuffs = new List<Debuff>();

    private void Awake()
    {
        MaxSpeed = speed;
    }

	// Use this for initialization
	void Start () {
        lastWaypointSwitchTime = Time.time;
        //MaxSpeed = speed;
        fire.SetActive(false);
        frost.SetActive(false);
        poison.SetActive(false);
        stun.SetActive(false);
        RotateIntoMoveDirection();
        
	}
	
	// Update is called once per frame
	void Update () {
        HandleDebuffs();
        Move();
	}

    public float MaxSpeed { get; set; }

    public void Spawn()
    {
        debuffs.Clear();
        Vector3 startPosition = waypoints[0].transform.position;
        transform.position = startPosition;
    }

    private void Move()
    {
        // 1 
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        Vector3 direction = Vector3.Normalize(endPosition - startPosition);

        float distance = Vector3.Distance(gameObject.transform.position, endPosition);

        //Debug.Log(distance);

        // 2 
        //float pathLength = Vector3.Distance(gameObject.transform.position, endPosition);

        //float totalTimeForPath = pathLength / speed;

        //float currentTimeOnPath = Time.time - lastWaypointSwitchTime;

        //gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if(!GameManager.Instance.Paused)
        gameObject.transform.position = gameObject.transform.position + direction * (speed/60);

        //Debug.Log(gameObject.transform.position);
        //Debug.Log(endPosition);

        // 3 
        //if (gameObject.transform.position.Equals(endPosition)) // enemy reached next waypoint 
        //if (gameObject.transform.position== endPosition)
        if (distance <= minimalDist)
        {
            //Debug.Log("reached");
            if (currentWaypoint < waypoints.Length - 2)
            {
                // 3.a 
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                // TODO: Rotate into move direction
               
                RotateIntoMoveDirection();
            }
            else
            {
                // 3.b 
                Destroy(gameObject);
                Handheld.Vibrate();

                //GameManager.Instance.enemyList.Remove(gameObject);
                //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                // TODO: deduct health
                if (GameObject.Find("Boss1(Clone)"))
                    GameManager.Instance.Health -= 5;
                else
                    GameManager.Instance.Health -= 1;
            }
        }
    }

    private void RotateIntoMoveDirection()
    {
        //1
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);
        //2
        float x = newDirection.x;
        float y = newDirection.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
        //3  
            GameObject sprite = (GameObject)
            gameObject.transform.FindChild("Sprite").gameObject;
            sprite.transform.rotation =
                Quaternion.AngleAxis(rotationAngle, Vector3.forward);

    }

    public float distanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }

    public void AddDebuff(Debuff debuff)
    {
        if (!debuffs.Exists(x => x.GetType() == debuff.GetType()))
        {
            newDebuffs.Add(debuff);
            ShowEffects(debuff);
        }
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffsToRemove.Add(debuff);
        HideEffects(debuff);
    }

    private void HandleDebuffs()
    {
        if(newDebuffs.Count > 0)
        {
            debuffs.AddRange(newDebuffs);

            newDebuffs.Clear();
        }

        foreach (Debuff debuff in debuffsToRemove)
        {
            debuffs.Remove(debuff);
        }

        debuffsToRemove.Clear();

        foreach(Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }

    public void TakeDamage(float damage)
    {
        Transform healthBarTransform = transform.FindChild("HealthBar");
        HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(damage, 0);

        if (healthBar.currentHealth <= 0)
        {
            int random_ = Random.Range(28, 35);
            Destroy(gameObject);
            //GameManager.Instance.enemyList.Remove(gameObject);
            //AudioSource audioSource = target.GetComponent<AudioSource>();
            //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            GameManager.Instance.Currency += random_;
            SoundManager.Instance.PlaySFX("coins");
            //SoundManager.Instance.PlaySFX("kill sound");
        }
    }

    public void ShowEffects(Debuff debuff)
    {
        Debuff tempDebuff = debuff;

        if(tempDebuff.ToString() == "FireDebuff" )
        {
            fire.SetActive(true);
        }
        else if(tempDebuff.ToString() == "FrostDebuff")
        {
            frost.SetActive(true);
        }
        else if(tempDebuff.ToString() == "PoisonDebuff")
        {
            poison.SetActive(true);
        }
        else if(tempDebuff.ToString() == "LightningDebuff")
        {
            stun.SetActive(true);
        }
    }

    public void HideEffects(Debuff debuff)
    {
        Debuff tempDebuff = debuff;

        if (tempDebuff.ToString() == "FireDebuff")
        {
            fire.SetActive(false);
        }
        else if (tempDebuff.ToString() == "FrostDebuff")
        {
            frost.SetActive(false);
        }
        else if (tempDebuff.ToString() == "PoisonDebuff")
        {
            poison.SetActive(false);
        }
        else if (tempDebuff.ToString() == "LightningDebuff")
        {
            stun.SetActive(false);
        }
    }
}
