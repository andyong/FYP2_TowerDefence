using UnityEngine;
using UnityEngine.Audio;
using System.Collections; 

public class Music : MonoBehaviour {
    private static Music instance = null;
    public static Music Instance 
    { get { return instance; } }

    void Awake() 
    { 
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        GameObject gameMusic = GameObject.Find("GameMusic");
        if (gameMusic)
        {
            Destroy(gameMusic);
        }
        //DontDestroyOnLoad(this.gameObject); 
    } 
}