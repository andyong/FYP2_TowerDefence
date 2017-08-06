using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour {

    public float speed = 0.1F;

    private float xMax;
    private float yMin;

	// Use this for initialization
	void Start () {

      

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed * Time.deltaTime, -touchDeltaPosition.y * speed * Time.deltaTime, 0);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax ), Mathf.Clamp(transform.position.y, yMin, 0),-10);

	}
   
    public void SetLimits(Vector3 maxTile)
    {
        
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;

     
    }
}
