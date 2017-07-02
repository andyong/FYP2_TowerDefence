using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Music2 : MonoBehaviour
{
    private static Music2 instance = null;
    public static Music2 Instance
    { get { return instance; } }


    //public AudioClip musicMainMenu;
    //public AudioClip musicLevel1;

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
        GameObject menuMusic = GameObject.Find("MenuMusic");
        if (menuMusic)
        {
            Destroy(menuMusic);
        }
        //DontDestroyOnLoad(this.gameObject);
    }
}