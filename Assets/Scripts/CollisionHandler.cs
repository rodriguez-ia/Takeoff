using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float invokeDelay = 2.5f;
    [SerializeField] AudioClip levelPassed;
    [SerializeField] AudioClip deathExplosion;

    Movement move;
    AudioSource audSource;

    bool isTransitioning = false;

    void Start()
    {
        move = GetComponent<Movement>();
        audSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                ;
                break;
            case "Finish":
                CompleteLevel();
                break;
            default: 
                Crash();
                break;
        }
    }

    void CompleteLevel()
    {
        isTransitioning = true;
        move.enabled = false;
        audSource.Stop();
        audSource.PlayOneShot(levelPassed);
        Invoke("LoadNextScene", invokeDelay);
    }

    void Crash()
    {
        isTransitioning = true;
        move.enabled = false;
        audSource.Stop();
        audSource.PlayOneShot(deathExplosion);
        Invoke("ReloadScene", invokeDelay);
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}