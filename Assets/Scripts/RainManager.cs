using System.Collections;
using UnityEngine;
using System;

// Using a Coroutine, an event is fired to tell sheeps they are infected
public class RainManager : MonoBehaviour
{
    public float rainInterval = 10f;

    public static bool isRaining = false;

    [SerializeField]
    Animator rainAnimator;

    // Using custom events to let any sheep know it's raining
    public static event Action<bool> InfectionEvent;

    void Start()
    {
        isRaining = false;
        StartCoroutine(toggleRain());
    }

    private void OnDestroy() 
    {
        isRaining = false;
        Debug.Log("Rain onDestroy");
        StopAllCoroutines();
        StopRain(); // Just in case it was raining
    }

    private IEnumerator toggleRain()
    {
        while (true)
        {
            if (isRaining)
                StartRain();
            else 
                StopRain();

            // Wait next toggle
            yield return new WaitForSeconds(rainInterval);
            isRaining = !isRaining;
        }
    }

    public void StartRain()
    {
        Debug.Log("It starts raining...");
        // Start rain animation
        rainAnimator.SetBool("isRaining", true);
        // Invoke event to infect
        InfectionEvent?.Invoke(true);
    }

    public void StopRain()
    {
        Debug.Log("...rain ended");
        // Stop rain animation
        rainAnimator.SetBool("isRaining", false);
        // Invoke event to disinfect
        InfectionEvent?.Invoke(false);
    }
}
