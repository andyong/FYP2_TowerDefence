using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour {

    public GameObject towerPrefab;
    private GameObject tower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

     
	
	}

    // Allow only one monster per location
    private bool canPlacetower()
    {
        return tower == null;
    }

    // Mouse up to place 
    void OnMouseUp()
    {
        if (canPlacetower())
        {
            tower = (GameObject)Instantiate(towerPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
        }     
    }
}
