using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public float rainInterval = 10f;
    private float timeLeft = 0f;

    bool isRaining = false;

    // UnityEvent to call events on sheep. 
    // Using custom BoolEvent as Brackeys does
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {}; // Event that passes a bool
    
    BoolEvent InfectionEvent;


    private void Awake() 
    {
        if (InfectionEvent == null)    
            InfectionEvent = new BoolEvent();
    }

    void Start()
    {
        isRaining = false;
        StartCoroutine(toggleRain());
    }

    private void OnDestroy() 
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
                InfectionEvent.Invoke(true);
            } 
            else 
            {
                // Start rain animation

                // Invoke event to infect
                InfectionEvent.Invoke(false);
            }
            // Wait and toggle rain
            yield return new WaitForSeconds(rainInterval);
            isRaining = !isRaining;
        }
    }
}
