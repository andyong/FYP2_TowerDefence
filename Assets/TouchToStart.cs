using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchToStart : MonoBehaviour {

    [SerializeField]
    private Button playbutt;

    [SerializeField]
    private Button settingbutt;

    [SerializeField]
    private Button helpbutt;

    [SerializeField]
    private Text touchTostart;

    [SerializeField]
    private float fadeTime;

    private Color textAlpha;

    bool fadein = false;
    bool fadeout = true;

	// Use this for initialization
	void Start () {

        playbutt.gameObject.SetActive(false);
        settingbutt.gameObject.SetActive(false);
        helpbutt.gameObject.SetActive(false);
        touchTostart.text = "Touch the screen to continue";

        textAlpha = touchTostart.color;
	
	}
	
	// Update is called once per frame
	void Update () {
       

        FadeinOut();

        if (Input.touchCount >= 1)
        {

            playbutt.gameObject.SetActive(true);
            settingbutt.gameObject.SetActive(true);
            helpbutt.gameObject.SetActive(true);
            touchTostart.gameObject.SetActive(false);
        }
	
	}

    void FadeinOut()
    {
        float rate = 1 / fadeTime;

        if(fadeout)
        {
            if (textAlpha.a > 0.1)
            {
               textAlpha.a -= rate * Time.deltaTime;
            }
            else
            {
                fadein = true;
                fadeout = false;
            }
                
        }
       
          
        if (fadein)
        {
            if (textAlpha.a < 1)
                textAlpha.a += rate * Time.deltaTime;
            else
            {
                fadein = false;
                fadeout = true;
            }
               
        }
           
        touchTostart.color = textAlpha;
    }

}
