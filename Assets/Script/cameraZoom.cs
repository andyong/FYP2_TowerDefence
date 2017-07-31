using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class cameraZoom : MonoBehaviour {

    float previousDistance;

    [SerializeField]
    private float zoomSpeed = 0.1f;

    public bool zoomIn = false;
    public bool zoomOut = false;

    private float camTemp=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began))
        {
            // calibrate distance
            previousDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }
        else if(Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
        {
            float distance;
            Vector2 touch1 = Input.GetTouch(0).position;
            Vector2 touch2 = Input.GetTouch(1).position;

            distance = Vector2.Distance(touch1, touch2);

            // camera based on pinch/zoom
            float pinchAmount = (previousDistance - distance) * zoomSpeed * Time.deltaTime; // < 0 = zoom in > 0 = zoom out

          
            if(Camera.main.orthographic)
            {
                if(Camera.main.orthographicSize >=5) // need to zoom in
                {
                    Camera.main.orthographicSize = 5;
                    zoomOut = false;
                    zoomIn = true;
                }
                else if(Camera.main.orthographicSize <=3) // need to zoom out 
                {
                    Camera.main.orthographicSize = 3;
                    zoomIn = false;
                    zoomOut = true;
                }

                if(zoomIn && pinchAmount <0)
                    Camera.main.orthographicSize = Camera.main.orthographicSize + pinchAmount;

                if(zoomOut && pinchAmount >0)
                    Camera.main.orthographicSize = Camera.main.orthographicSize + pinchAmount;
             
                //else if(Camera.main.orthographicSize >= 5)
                //    Camera.main.orthographicSize = 4.99f;
                //else if (Camera.main.orthographicSize <= 3)
                //    Camera.main.orthographicSize = 3.01f;
                   
                //else if(Camera.main.orthographicSize ==3)
                //{
                //    Camera.main.orthographicSize = 3.1f;
                //}
                //else if (Camera.main.orthographicSize == 5)
                //{
                //    Camera.main.orthographicSize = 4.9f;
                //}
                
            }
            else
            {
                Camera.main.transform.Translate(0, 0, pinchAmount);
            }

            previousDistance = distance;
        }
	}
}
