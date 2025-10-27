using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private GameObject RedballoonPrefab;
    [SerializeField] private GameObject YellowballoonPrefab;
    [SerializeField] private Transform spawnPoint;

    private int BalloonID = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BalloonID++;
            GameObject currentObject = Instantiate(YellowballoonPrefab, spawnPoint.position, Quaternion.identity);
            var movementScript = currentObject.GetComponent<balloonMovementScript>();
            movementScript.myWaypoints = waypoints;
            movementScript.BalloonID = BalloonID;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BalloonID++;
            GameObject currentObject = Instantiate(RedballoonPrefab, spawnPoint.position, Quaternion.identity);
            var movementScript = currentObject.GetComponent<balloonMovementScript>();
            movementScript.myWaypoints = waypoints;
            movementScript.BalloonID = BalloonID;
        }
    }
}
