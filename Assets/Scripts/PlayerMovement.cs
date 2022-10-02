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

    }

    private void FixedUpdate() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement *= speed * Time.deltaTime;

        if (movement.magnitude > 0.001f)
        {
            // Debug.Log(movement);
            transform.Translate(movement);
        }
    }
}
