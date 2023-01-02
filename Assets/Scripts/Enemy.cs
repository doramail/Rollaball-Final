using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0;
    public List<Transform> waypoint;

    private int waypointIndex;
    private float range;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        EnnemyMove();
    }

    void EnnemyMove() 
    {
        transform.LookAt(waypoint[waypointIndex]);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypoint[waypointIndex].position) < range )
        {
            waypointIndex++;
            if (waypointIndex >= waypoint.Count )
            {
                waypointIndex = 0;
            }
        }
    }
}
