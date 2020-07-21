using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class LevelControl : MonoBehaviour
{

    public int index;
    public string levelName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Loads scene by index in build
            //SceneManager.LoadScene(index);

            //Loads scene by name
            SceneManager.LoadScene(levelName);

        }
    }

}