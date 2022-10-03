using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If any sheep is in range, push them away when pressing B
public class ScareSheepInRange : MonoBehaviour
{
    List<GameObject> sheepInRange, wolvesInRange;
    public float barkStrength = 0f;
    Vector2 barkForce;

    [SerializeField]
    Transform dog;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip barkSound;

    // Start is called before the first frame update
    void Start()
    {
        sheepInRange = new List<GameObject>();
        wolvesInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // When the dog barks, sheeps in bark range must be pushed away
        if (Input.GetButtonDown("Bark") && sheepInRange.Count > 0)
        {
            // Play sound
            audioSource.PlayOneShot(barkSound);
            
            // In there are wolves, detroy joint with sheeps
            foreach (GameObject wolf in wolvesInRange)
            {
                if (wolf != null)
                {
                    wolf.GetComponent<WolfAI>()?.destroyJoint();
                }
            }

            foreach (GameObject sheep in sheepInRange)
            {
                // Skip if the sheep has been mutated/destroyed but we had still it in range
                if (sheep != null)
                {
                    Rigidbody2D sheepRb = sheep.GetComponent<Rigidbody2D>();
                    if (sheepRb != null)
                    {
                        barkForce = barkStrength * (dog.position - sheep.transform.position).normalized;
                        sheepRb.AddForce(-barkForce, ForceMode2D.Impulse);
                    }
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Sheep"))
            sheepInRange.Add(other.gameObject);
        if (other.gameObject.CompareTag("Wolf"))
            wolvesInRange.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
    
        if (other.gameObject.CompareTag("Sheep"))
            sheepInRange.Remove(other.gameObject);
            // if (sheepInRange.Remove(other.gameObject))
            //     Debug.Log("Sheep: "+other.gameObject.name + " removed from sheep in range");
    }
}
