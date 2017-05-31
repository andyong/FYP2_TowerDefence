using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
    public float speed = 10; //of bullets
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private UIManager uiManager;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        uiManager = gm.GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
        // 1 
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2 
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                 //3
                Transform healthBarTransform = target.transform.FindChild("HealthBar");
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                // 4
                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    //AudioSource audioSource = target.GetComponent<AudioSource>();
                    //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    uiManager.Gold += 50;
                }
            }
            Destroy(gameObject);
        }
	}
}
