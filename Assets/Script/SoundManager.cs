using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField]
    private AudioSource sfxSource;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

	// Use this for initialization
	void Start () {

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio") as AudioClip[];

        foreach ( AudioClip clip in clips)
        {
            audioClips.Add(clip.name, clip);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySFX(string name)
    {
        sfxSource.PlayOneShot(audioClips[name]);
    }
}
