using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class balloonMovementScript : MonoBehaviour
{
    [SerializeField] private GameObject popEffect;

    public List<Transform> myWaypoints;
    public float myWaypointDistance = 0;
    private int currentWaypointIndex = 0;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (myWaypoints == null || myWaypoints.Count == 0)
            return;

        Transform targetWaypoint = myWaypoints[currentWaypointIndex];
        Vector3 previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Increase myWaypointProcentage by the distance moved this frame
        myWaypointDistance += Vector3.Distance(previousPosition, transform.position);

        //Debug.Log("My location: " + transform.position + "   Moving to waypoint: " + targetWaypoint.position);
        //Debug.Log("My waypoint percentage: " + myWaypointDistance);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.01f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % myWaypoints.Count;
        }
    }

    public void PopBalloon()
    {
        Instantiate(popEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
