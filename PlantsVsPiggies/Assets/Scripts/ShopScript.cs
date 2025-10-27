using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject shovelAnim;
    [SerializeField] private EconomyScript economyScript;

    [System.Serializable]

    public struct FlowerButtonData
    {
        public Button button;
        public int price;
        public GameObject flowerPrefab;
        public GameObject flowerToPlacePrefab;
    }

    [SerializeField] private List<FlowerButtonData> flowerButtons;

    private GameObject spawnedFlower;
    private GameObject flowerToPlace;
    private bool isDragging = false;

    void Start()
    {
        foreach (var flowerButton in flowerButtons)
        {
            flowerButton.button.GetComponentInChildren<TextMeshProUGUI>().text = flowerButton.price.ToString() + "$";
            AddEventTriggers(flowerButton.button, flowerButton.price, flowerButton.flowerPrefab, flowerButton.flowerToPlacePrefab);
        }
    }

    void Update()
    {
        if (isDragging && spawnedFlower != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Distance from camera
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            spawnedFlower.transform.position = worldPos;
        }
    }

    private void AddEventTriggers(Button button, int price, GameObject flowerPrefab, GameObject flowerToPlacePrefab)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        // Pointer Down
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((data) => { OnFlowerButtonDown(flowerPrefab, flowerToPlacePrefab, price); });
        trigger.triggers.Add(pointerDownEntry);

        // Pointer Up
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((data) => { OnFlowerButtonUp(price); });
        trigger.triggers.Add(pointerUpEntry);
    }

    private void OnFlowerButtonDown(GameObject flowerPrefab, GameObject flowerToPlacePrefab, int price)
    {
        if (economyScript.Coins >= price)
        {
            if (spawnedFlower == null)
            {
                spawnedFlower = Instantiate(flowerPrefab);
                flowerToPlace = flowerToPlacePrefab;
            }
            isDragging = true;
        }
    }

    private void OnFlowerButtonUp(int price)
    {
        isDragging = false;
        // Instantiate shovel animation

        if (spawnedFlower == null)
        {
            return;
        }

        if (spawnedFlower.GetComponent<collisionCheckerScript>().collisions > 0)
        {
            Destroy(spawnedFlower);
            spawnedFlower = null;
            return;
        }
        economyScript.Coins -= price;
        GameObject shovelInst = Instantiate(shovelAnim, spawnedFlower.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);

        // Start coroutine to handle delayed placement
        StartCoroutine(PlacePlantAfterDelay(0.92f, spawnedFlower.transform.position));

        Destroy(shovelInst, 0.92f);
        Destroy(spawnedFlower, 0.92f);
        spawnedFlower = null;
    }

    private IEnumerator PlacePlantAfterDelay(float delay, Vector3 position)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(flowerToPlace, position, Quaternion.identity);
    }
}
