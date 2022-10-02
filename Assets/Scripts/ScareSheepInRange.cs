using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If any sheep is in range, push them away when pressing B
public class ScareSheepInRange : MonoBehaviour
{
    List<GameObject> sheepInRange;
    public float barkStrength = 0f;
    Vector2 barkForce;

    [SerializeField]
    Transform dog;

    // Start is called before the first frame update
    void Start()
    {
        sheepInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // When the dog barks, sheeps in bark range must be pushed away
        if (Input.GetButtonDown("Bark") && sheepInRange.Count > 0)
        {
            foreach (GameObject sheep in sheepInRange)
            {
                // Skip if the sheep has been mutated/destroyed but we had still it in range
                if (sheep != null)
                {
                    Rigidbody2D sheepRb = sheep.GetComponent<Rigidbody2D>();
                    if (sheepRb != null)
                    {
                        barkForce = barkStrength * -(dog.position - sheep.transform.position);
                        sheepRb.AddForce(barkForce);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Sheep"))
            sheepInRange.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
    
        if (other.gameObject.CompareTag("Sheep"))
            sheepInRange.Remove(other.gameObject);
            // if (sheepInRange.Remove(other.gameObject))
            //     Debug.Log("Sheep: "+other.gameObject.name + " removed from sheep in range");
    }
}
