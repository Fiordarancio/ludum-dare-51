using System.Collections;
using UnityEngine;
using System;

// Using a Coroutine, an event is fired to tell sheeps they are infected
public class RainManager : MonoBehaviour
{
    public float rainInterval = 10f;

    bool isRaining = false;
    [SerializeField]
    GameObject backgroundSunny;
    [SerializeField]
    GameObject backgroundRainy;

    // Using custom events to let any sheep know it's raining
    public static event Action<bool> InfectionEvent;


    void Start()
    {
        isRaining = false;
        StartCoroutine(toggleRain());
    }

    private void OnDestroy() 
    {
        InfectionEvent?.Invoke(false);
        StopAllCoroutines();
    }
    private void OnDisable() 
    {
        InfectionEvent?.Invoke(false);
        StopAllCoroutines();    
    }

    private IEnumerator toggleRain()
    {
        while (true)
        {
            if (isRaining)
            {
                // Stop rain animation
                backgroundRainy.SetActive(true);
                backgroundSunny.SetActive(false);
                // Invoke event to disinfect
                InfectionEvent?.Invoke(true);
            } 
            else 
            {
                // Stop rain animation
                backgroundRainy.SetActive(false);
                backgroundSunny.SetActive(true);
                // Invoke event to disinfect
                InfectionEvent?.Invoke(false);
            }
            // Wait and toggle rain
            yield return new WaitForSeconds(rainInterval);
            isRaining = !isRaining;
        }
    }
}
