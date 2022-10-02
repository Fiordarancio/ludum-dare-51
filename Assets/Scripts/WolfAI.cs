using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WolfAI : MonoBehaviour
{
    
    public float speed = 3f;
    public float nextWaypointDistance = 1f;

    Transform target = null;
    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("Eaten!");
            Destroy(other.gameObject);
        }
    }
}
