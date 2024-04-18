//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AISpawner : MonoBehaviour
//{
//    // Start is called before the first frame update
//    public int AICount = 5;
//    public GameObject AIObject;

//    void Start()
//    {
//        for (int i = 0; i < AICount; i++)
//        {
//            var pos = GetPositionForAI();
//            var ai = Instantiate(AIObject, pos, Quaternion.identity);
//            ai.transform.LookAt(Vector3.zero);
//        }
//    }
//    Vector3 GetPositionForAI()
//    {
//        while (true)
//        {
//            var pos = new Vector3(Random.Range(-5.0f, 5.0f), 1, Random.Range(-5.0f, 5.0f));
//            var colliders = Physics.OverlapBox(pos, new Vector3(1, 0.5f, 1));
//            if (colliders.Length == 0) return pos;
//        }
//    }
//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AISpawner : MonoBehaviour
//{
//    public int AICount = 5;
//    public GameObject AIObject;

//    void Start()
//    {
//        // Start coroutine to instantiate AI objects
//        StartCoroutine(SpawnAIs());
//    }

//    IEnumerator SpawnAIs()
//    {
//        if (AIObject == null)
//        {
//            Debug.LogError("AIObject is not assigned in the inspector");
//            yield break;
//        }

//        for (int i = 0; i < AICount; i++)
//        {
//            Vector3 pos = GetPositionForAI();
//            if (pos != Vector3.zero) // Ensure we have a valid position
//            {
//                var ai = Instantiate(AIObject, pos, Quaternion.identity);
//                ai.transform.LookAt(Vector3.zero);
//            }
//            yield return new WaitForSeconds(0.5f); // Wait half a second before spawning the next AI
//        }
//    }

//    Vector3 GetPositionForAI()
//    {
//        int maxAttempts = 100;
//        for (int attempts = 0; attempts < maxAttempts; attempts++)
//        {
//            Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 1, Random.Range(-5.0f, 5.0f));
//            Collider[] colliders = Physics.OverlapBox(pos, new Vector3(1, 0.5f, 1));
//            if (colliders.Length == 0)
//                return pos;
//        }
//        Debug.LogWarning("Failed to find a free position for AI");
//        return Vector3.zero; // Return zero vector if a position isn't found
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    
    // Public variables that can be set from the Unity Editor.
    public int AICount = 5;  // Number of AI objects to spawn.
    public GameObject AIObject;  // The AI GameObject prefab to be instantiated.
    public LayerMask spawnLayer;  // Only consider these layers for collision checks during AI spawning.

    // Start is called before the first frame update
    void Start()
    {
        // Starts the coroutine that handles AI spawning.
        StartCoroutine(SpawnAIs());
    }

    // Coroutine that spawns AI objects
    IEnumerator SpawnAIs()
    {
        // Check if the AI prefab is assigned
        if (AIObject == null)
        {
            // Log an error if AIObject has not been assigned in the inspector
            Debug.LogError("AIObject is not assigned in the inspector");
            yield break;  // Stop the coroutine if AIObject is not assigned
        }

        // Loop to spawn the specified number of AIs
        for (int i = 0; i < AICount; i++)
        {
            // Get a position for spawning the AI
            Vector3 pos = GetPositionForAI();
            // Check if a valid position was found
            if (pos != Vector3.zero)
            {
                // Instantiate the AI at the found position with no rotation
                var ai = Instantiate(AIObject, pos, Quaternion.identity);
                // Make the instantiated AI face the world origin (0,0,0)
                ai.transform.LookAt(Vector3.zero);
            }
            // Wait for 0.5 seconds before spawning the next AI
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Method to find a suitable spawning position for an AI
    Vector3 GetPositionForAI()
    {
        // Set a limit on the number of attempts to find a spawn position
        int maxAttempts = 100;
        // Define the search area radius
        float searchRadius = 10.0f;
        // Define the size of the box used for checking overlaps
        Vector3 boxSize = new Vector3(1, 0.5f, 1);

        // Attempt to find a spawn position up to maxAttempts times
        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            // Randomly choose a position within the search area
            Vector3 pos = new Vector3(Random.Range(-searchRadius, searchRadius), 1, Random.Range(-searchRadius, searchRadius));
            // Check for any colliders overlapping with a box placed at the chosen position
            Collider[] colliders = Physics.OverlapBox(pos, boxSize, Quaternion.identity, spawnLayer);
            // If no colliders are found, return this position as a valid spawn location
            if (colliders.Length == 0)
                return pos;
        }
        // If no valid position is found after all attempts, log a warning
        Debug.LogWarning("Failed to find a free position for AI");
        // Return a zero vector indicating failure to find a valid position
        return Vector3.zero;
    }
}
