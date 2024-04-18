using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   bool hasKey = false;
    public string nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true; //this needs to be a game data?????
            Destroy(other.gameObject);

        }

        else if (other.CompareTag("Door"))
        {
            if (hasKey)
            {
                Debug.Log("Next Level!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
            }
        }
        else if (other.CompareTag("AI")) 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("YouLose");
        } 
    }
}
