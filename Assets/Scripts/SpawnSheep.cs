using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSheep : MonoBehaviour
{
    [SerializeField]
    float minScale = 0.1f;
    [SerializeField, Range(0f,1f)]
    float targetScale = 1.0f;      
    [SerializeField]
    float spawnTime = 0.5f;     // Time to reach normal size

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.localScale = Vector3.Lerp(minScale*Vector3.one, targetScale, spawnTime);
    }

    // private IEnumerator Spawn()
    // {
        
    // }
}
