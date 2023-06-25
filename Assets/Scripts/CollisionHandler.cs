using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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
                LoadLevel();
                break;
            case "Floor":
                ReladLevel();
                break;

        }
    }

    private void ReladLevel()
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
