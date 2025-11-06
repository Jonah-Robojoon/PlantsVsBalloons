using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private GameObject RedballoonPrefab;
    [SerializeField] private GameObject YellowballoonPrefab;
    [SerializeField] private Transform spawnPoint;

    // Wave parameters
    private float redSpawnInterval = 1f;
    private float yellowSpawnInterval = 1f;
    private int currentWave = 0;
    private int redBalloonsPerWave = 2;
    private int yellowBalloonsPerWave = 5;

    [SerializeField] Button startWaveButton;

    private int BalloonID = 0;

    // Pseudocode plan:
    // 1. In Update(), check if there are no GameObjects with tag "balloon".
    // 2. If none exist, enable startWaveButton (set interactable = true).
    // 3. If startWaveButton is clicked, call NextWave() and disable the button until all balloons are gone again.
    // 4. Add a listener for startWaveButton.onClick in Start().

    void Start()
    {
        startWaveButton.onClick.AddListener(OnStartWaveButtonClicked);
        startWaveButton.interactable = false;
    }

    void Update()
    {
        // Check if all balloons are gone
        if (GameObject.FindGameObjectsWithTag("Balloon").Length == 0)
        {
            startWaveButton.interactable = true;
        }
        else
        {
            startWaveButton.interactable = false;
        }

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

    private void OnStartWaveButtonClicked()
    {
        NextWave();
        startWaveButton.interactable = false;
    }


    private void NextWave()
    {
        Debug.Log("Starting wave " + (currentWave + 1));

        yellowBalloonsPerWave = Mathf.CeilToInt(yellowBalloonsPerWave * 1.2f);
        redBalloonsPerWave = yellowBalloonsPerWave / 4;
        yellowSpawnInterval *= 0.9f;
        redSpawnInterval = yellowSpawnInterval * 0.8123f;
        currentWave++;

        StartCoroutine(RedWave());
        StartCoroutine(YellowWave());
    }

    private IEnumerator RedWave()
    {
        for (int b = 0; b < redBalloonsPerWave; b++)
        {
            SpawnRedBalloon();
            yield return new WaitForSeconds(redSpawnInterval);
        }
    }

    private IEnumerator YellowWave()
    {
        for (int b = 0; b < yellowBalloonsPerWave; b++)
        {
            SpawnYellowBalloon();
            yield return new WaitForSeconds(yellowSpawnInterval);
        }
    }



    private void SpawnYellowBalloon()
    {
        Debug.Log("Spawning Yellow Balloon");

        BalloonID++;
        GameObject currentObject = Instantiate(YellowballoonPrefab, spawnPoint.position, Quaternion.identity);
        var movementScript = currentObject.GetComponent<balloonMovementScript>();
        movementScript.myWaypoints = waypoints;
        movementScript.BalloonID = BalloonID;
    }

    private void SpawnRedBalloon()
    {
        BalloonID++;
        GameObject currentObject = Instantiate(RedballoonPrefab, spawnPoint.position, Quaternion.identity);
        var movementScript = currentObject.GetComponent<balloonMovementScript>();
        movementScript.myWaypoints = waypoints;
        movementScript.BalloonID = BalloonID;
    }
}
