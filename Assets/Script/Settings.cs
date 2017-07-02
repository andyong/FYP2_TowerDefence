using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public Slider volSlider;
	// Use this for initialization
	void Start () {
        //volSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}
	
    //// Update is called once per frame
    //void Update () {
	
    //}
    public void OnValueChanged()
    {
        AudioListener.volume = volSlider.value;
    }
}
