using System.Collections;
using UnityEngine;
using System;

// Using a Coroutine, an event is fired to tell sheeps they are infected
public class RainManager : MonoBehaviour
{
    public float rainInterval = 10f;

    bool isRaining = false;

    [SerializeField]
    Sprite backgroundSunny;
    [SerializeField]
    Sprite backgroundRainy;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    // Using custom events to let any sheep know it's raining
    public static event Action<bool> InfectionEvent;

    void Start()
    {
        isRaining = false;
        StartCoroutine(toggleRain());
    }

    private void OnDestroy() 
    {
        StopRain();
        StopAllCoroutines();
    }
    private void OnDisable() 
    {
        StopRain();
        StopAllCoroutines();    

    }

    private IEnumerator toggleRain()
    {
        while (true)
        {
            if (isRaining)
            {
                // Start rain animation
                spriteRenderer.sprite = backgroundRainy;
                // Invoke event to infect
                StartRain();
            }
            else 
            {
                // Stop rain animation
                spriteRenderer.sprite = backgroundSunny;
                // Invoke event to disinfect
                StopRain();
            }
                StopRain();

            // Wait next toggle
            yield return new WaitForSeconds(rainInterval);
            isRaining = !isRaining;
        }
    }

    public void StartRain()
    {
        InfectionEvent?.Invoke(true);
    }

    public void StopRain()
    {
        InfectionEvent?.Invoke(false);
    }
}
