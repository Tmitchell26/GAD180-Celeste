using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class Trap : MonoBehaviour
{
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Loads scene by name
            //SceneManager.LoadScene(levelName);

            //Restart level when player enters area
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
