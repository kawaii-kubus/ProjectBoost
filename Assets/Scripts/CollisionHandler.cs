using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelTimer = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip win;

    [SerializeField] ParticleSystem sucessParticle;
    [SerializeField] ParticleSystem crashParticle;



    AudioSource myAudio;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Update()
    {
        nextLevelKeyL();
        DisableCollisionKeyC();

    }
    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {   if (isTransitioning || collisionDisable) { return; }
        
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Hitted Friendly");
                    break;
                case "Fuel":
                    Debug.Log("You collected fuel");
                    break;
                case "Finish":
                    FinishLevel();
                    break;
                case "Floor":
                    StartCrashSequence();
                    break;

            
        }

    }

    private void StartCrashSequence()
    {
        //todo add SFX upon crash
        // todo add particle effect upon crash
        isTransitioning = true;
        myAudio.Stop();
        myAudio.PlayOneShot(crash);
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelTimer);
        crashParticle.Play();


    }

    private void FinishLevel()
    {
        //todo add SFX upon finish
        // todo add particle effect upon finish
        isTransitioning = true;
        myAudio.Stop();
        Invoke("LoadLevel", LevelTimer);
        gameObject.GetComponent<Movement>().enabled = false;
        myAudio.PlayOneShot(win);
        sucessParticle.Play();



    }
    private void ReloadLevel()
    {
        int lvl = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(lvl);
    }

    private void LoadLevel()
    {
        int currentLvl = SceneManager.GetActiveScene().buildIndex;
        int nextLvl = currentLvl + 1;
        if (nextLvl == SceneManager.sceneCountInBuildSettings)
        {
            nextLvl = 0;
        }
        SceneManager.LoadScene(nextLvl);
    }

    private void nextLevelKeyL()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel();
        }
    }

    private void DisableCollisionKeyC()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;

        }
    }
}
