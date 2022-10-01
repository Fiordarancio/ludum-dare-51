using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 force;

    [SerializeField]
    private float speed = 100f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 force = movement * speed * Time.deltaTime;

        if (movement.magnitude > 0)
        {
            Debug.Log(force);

            // rb.AddForce(force);
            transform.Translate(force);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            
        }
    }
}
