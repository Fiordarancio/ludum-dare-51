using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{

    Vector3 dir;
    public float speed = 200f;
    public Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dir = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        // Move randomly
        dir.x = Random.Range(-1f, +1f);
        dir.y = Random.Range(-1f, +1f);

        // transform.Translate(dir * speed * Time.deltaTime);
        rb.AddForce(dir * speed * Time.deltaTime);
    }
}
