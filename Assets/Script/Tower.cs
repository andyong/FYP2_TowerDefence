using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    private SpriteRenderer mySpriteRenderer;

    private SpawnEnemy target;

    [SerializeField]
    public GameObject bullet;
    //public float fireRate;

	// Use this for initialization
	void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(target);
	}

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            target = other.GetComponent<SpawnEnemy>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            target = null;
        }
    }
}
