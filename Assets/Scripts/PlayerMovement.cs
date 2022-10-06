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

    private bool isFacingRight = true;
    [SerializeField]
    Transform sprite; // the dog sprite to be be flipped

    private void FixedUpdate() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement *= speed * Time.deltaTime;

        if (movement.magnitude > 0.001f)
        {
            // Debug.Log(movement);
            transform.Translate(movement);

            // If the input is moving the player right and the player is facing left...
			if (movement.x > 0 && !isFacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (movement.x < 0 && isFacingRight)
			{
				// ... flip the player.
				Flip();
			}
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		// Multiply the sprite's x local scale by -1.
        Vector3 spriteScale = sprite.transform.localScale;
        spriteScale.x *= -1;
        sprite.transform.localScale = spriteScale;
    }
}
