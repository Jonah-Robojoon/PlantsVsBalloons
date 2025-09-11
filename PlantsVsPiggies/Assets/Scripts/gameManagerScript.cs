using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            GameObject currentObject = Instantiate(balloonPrefab, spawnPoint.position, Quaternion.identity);
            var movementScript = currentObject.GetComponent<balloonMovementScript>();
            movementScript.myWaypoints = waypoints;
        }
    }
}
