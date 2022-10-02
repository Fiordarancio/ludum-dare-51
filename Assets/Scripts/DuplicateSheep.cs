using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateSheep : MonoBehaviour
{
    private bool isInfected = false;

    [SerializeField]
    GameObject SheepPrefab;
    [SerializeField]
    GameObject WolfPrefab;

    [Range(0f,1f)]
    public float wolfProbability = 0.2f;
    public float spawnSheepOffset = 1.0f;    // Dinstance of spawning for new sheep
    
    // TEST
    [Header("Test")]
    public float infectionTime = 10f;       // Total time in which a mutation or duplication happens
    public float mutationTime = 2f;         // Time in which we play animation or mutation

    // Start is called before the first frame update
    void Start()
    {
        isInfected = false; 
        infectionTime = (infectionTime > mutationTime)? infectionTime : 3f;

        // Register for Infect event
        GameManager.InfectionEvent += onInfect;
    }

    // Event to undestand if rain affected the sheep. Infection is controlled by game manager
    private void onInfect (bool infect)
    {
        isInfected = infect;
        if (isInfected == true)
        {
            Debug.Log(gameObject.name + " infected now!");
            StartCoroutine(ApplyInfection());
        }
        // else
        //     StopAllCoroutines(); // Shouldn't be necessary
    }

    // Unsubscribe from InfectionEvent when destroyed or an error will popup
    private void OnDisable() 
    {
        GameManager.InfectionEvent -= onInfect;    
    }

    // Thread for duplicating sheeps every 10 seconds
    private IEnumerator ApplyInfection() 
    {
        while (isInfected) 
        {
            yield return new WaitForSeconds(infectionTime-mutationTime);
            // Evaluate probability for spawning wolf (20%)
            bool isWolf = Random.Range(0f, 1f) <= wolfProbability;
            // Instantiate a wolf or a couple of sheep
            if (isWolf)
            {
                // Activate animation for mutation 
                // TODO: enable dog sensing: sheep does is no more pushable
                gameObject.tag = "Sheep2Wolf";

                // Wait a bit then mutate and die
                Debug.Log("Mutating...");
                yield return new WaitForSeconds(mutationTime);
                Instantiate(WolfPrefab, transform.position, transform.rotation);
                Debug.Log("...done!");
                isInfected = false; // Should not be necessary
                Destroy(this.gameObject);
            }
            else
            {
                // Activate animation for duplication

                // Wait a bit then create a duplicate (don't destroy yourself)
                Debug.Log("Duplication...");
                yield return new WaitForSeconds(mutationTime);
                Instantiate(SheepPrefab, spawnPosition(), transform.rotation);
                Debug.Log("...complete!");
            }
        }
        Debug.Log("Ending infection.");
    }

    // If there's another object in the spawn position, rigidbodys and colliders 
    // automatically prevent overlapping.
    private Vector3 spawnPosition() 
    {
        Vector3 spawnPos = Vector3.zero;
        Vector3 dir;

        // If we are too close to zero, spawn on right or left
        if (transform.position.magnitude < 0.1f)
        {
            dir = Vector3.right;
        }
        else
        {
            dir = -Vector3.Normalize(transform.position);
        }

        spawnPos = transform.position + spawnSheepOffset * dir;
        Debug.Log("Position: "+transform.position.x+", "+transform.position.y+" new: "+spawnPos.x+", "+spawnPos.y);
        return spawnPos;
    }


}
