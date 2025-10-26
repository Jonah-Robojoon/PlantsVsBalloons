using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private GameObject RedballoonPrefab;
    [SerializeField] private Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            GameObject currentObject = Instantiate(RedballoonPrefab, spawnPoint.position, Quaternion.identity);
            var movementScript = currentObject.GetComponent<balloonMovementScript>();
            movementScript.myWaypoints = waypoints;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
        }
    }
}
