using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{

    public float rainInterval = 10f;

    bool isRaining = false;

    // Using custom events to let any sheep know it's raining
    public static event Action<bool> InfectionEvent;


    void Start()
    {
        isRaining = false;
        StartCoroutine(toggleRain());
    }

    private void OnDestroy() 
    {
        StopAllCoroutines();
    }
    private void OnDisable() 
    {
        StopAllCoroutines();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator toggleRain()
    {
        while (true)
        {
            if (isRaining)
            {
                // Stop rain animation

                // Invoke event to disinfect
                InfectionEvent?.Invoke(true);
            } 
            else 
            {
                // Start rain animation

                // Invoke event to infect
                InfectionEvent?.Invoke(false);
            }
            // Wait and toggle rain
            yield return new WaitForSeconds(rainInterval);
            isRaining = !isRaining;
        }
    }
}
