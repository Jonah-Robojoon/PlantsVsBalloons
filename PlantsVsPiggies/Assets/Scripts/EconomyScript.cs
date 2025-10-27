using TMPro;
using UnityEngine;

public class EconomyScript : MonoBehaviour
{
    [SerializeField] private GameObject coinText;
    [SerializeField] private int StartingMoney = 10;
    [SerializeField] private int startingHealth = 100;
    private int Health;
    private int coins;

    private void Start()
    {
        Coins = StartingMoney;
        Health = startingHealth;
        coinText.GetComponent<TextMeshProUGUI>().text = Coins.ToString();
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

    private void Update()
    {
        if (Health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
