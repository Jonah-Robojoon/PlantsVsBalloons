using TMPro;
using UnityEngine;

public class EconomyScript : MonoBehaviour
{
    [SerializeField] private GameObject coinText;
    [SerializeField] private GameObject healthText;
    [SerializeField] private int StartingMoney = 10;
    [SerializeField] private int startingHealth = 100;
    private int health;
    private int coins;

    private void Start()
    {
        Coins = StartingMoney;
        health = startingHealth;
        coinText.GetComponent<TextMeshProUGUI>().text = Coins.ToString();
        healthText.GetComponent<TextMeshProUGUI>().text = "Lives " + Lives.ToString();
    }

    public int Coins
    {
        get { return coins; }
        set 
        {
            coins =+ value;

            coinText.GetComponent<TextMeshProUGUI>().text = Coins.ToString();
        }
    }

    public int Lives
    {
        get { return health; }
        set
        {
            health =+ value;

            healthText.GetComponent<TextMeshProUGUI>().text = "Lives " + health.ToString();
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Application.Quit();
        }
    }
}
