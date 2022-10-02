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
    public float every10seconds = 10f;
    public float mutationTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        isInfected = false; 
        every10seconds = (every10seconds > mutationTime)? every10seconds : 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfected ()
    {
        isInfected = !isInfected;
        if (isInfected == true)
            StartCoroutine(ApplyInfection());
        // else
        //     StopAllCoroutines(); // Shouldn't be necessary
    }

    // Thread for duplicating sheeps every 10 seconds
    private IEnumerator ApplyInfection() 
    {
        while (isInfected) 
        {
            yield return new WaitForSeconds(every10seconds-mutationTime);
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
                isInfected = false;
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

        // Should use a pathfinding. For now, spawn on the direction to center (actually -pos)
        spawnPos = transform.position - spawnSheepOffset*transform.position/transform.position.magnitude; 
        return spawnPos;
    }


}
