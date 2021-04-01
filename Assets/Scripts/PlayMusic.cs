using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    AudioSource audSource;

    static bool isPlayingSoundtrack = false;


    void Start()
    {
        audSource = GetComponent<AudioSource>();
        PlaySoundtrack();
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void PlaySoundtrack()
    {
        if(!isPlayingSoundtrack)
        {
            audSource.Play();
            isPlayingSoundtrack = true;
        }
    }
}
