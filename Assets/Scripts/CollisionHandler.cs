using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelTimer = 1f;
    private void OnCollisionEnter(Collision collision)
    {
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
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelTimer);
    }

    private void FinishLevel()
    {
        //todo add SFX upon finish
        // todo add particle effect upon finish
        Invoke("LoadLevel", LevelTimer);
        gameObject.GetComponent<Movement>().enabled = false;
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
}
