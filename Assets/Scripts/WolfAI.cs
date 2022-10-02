using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WolfAI : MonoBehaviour
{
    public float speed = 3f;
    public float nextWaypointDistance = 1f;
    public float eatingTime = 2f;

    Seeker seeker;
    Rigidbody2D rb;

    Transform target = null;
    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Collision2D contactSheep = null;
    float contactTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && target != null)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update() {
        GameObject[] sheeps = null;

        if (target == null)
            sheeps = GameObject.FindGameObjectsWithTag("Sheep");

            if (sheeps == null)
                return;

            GameObject minSheep = null;
            float minDist = 1000f;

            foreach (GameObject sheep in sheeps)
            {
                float curDist = ((Vector2)sheep.transform.position - rb.position).magnitude;

                if (curDist < minDist)
                {
                    minDist = curDist;
                    minSheep = sheep;
                }
            }

            if (minSheep != null)
                target = minSheep.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (force.magnitude > 0.001f && contactSheep == null)
            transform.Translate(force);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("Bon appetit!");
            contactSheep = other;
            contactTimer = Time.time;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sheep"))
        {
            if (contactTimer + eatingTime < Time.time)
            {
                Debug.Log("Eaten!");
                Destroy(contactSheep.gameObject);
            } else
            {
                Debug.Log("Eating!");
                Debug.Log(Time.time);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("Eaten or escaped!");
            contactSheep = null;
            contactTimer = 0f;
        }
    }
}