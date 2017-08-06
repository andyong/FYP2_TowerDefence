using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{

    public MoveEnemy target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private Tower parent;
    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
    }

    public void Initialize(Tower parent)
    {
        //this.target = parent.Target;
        this.parent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * parent.Speed / distance);

        // 2 
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                //Debug.Log(parent.Damage);
                //3
                target.TakeDamage(parent.Damage);
                ApplyDebuff();
                // 4
            }
            Destroy(gameObject);

        }
        
    }

    private void ApplyDebuff()
    {
        float roll = Random.Range(0, 100);
        if (roll <= parent.Proc)
        {
            target.AddDebuff(parent.GetDebuff());
           
        }
   
    }
}
