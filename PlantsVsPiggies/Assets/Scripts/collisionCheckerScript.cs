using UnityEngine;

public class collisionCheckerScript : MonoBehaviour
{
    public int collisions = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map") || collision.CompareTag("Plant"))
        {
            collisions++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Map") || collision.CompareTag("Plant"))
        {
            collisions--;
        }
    }



    private void Update()
    {
        if (collisions > 0)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f); // Red with 50% opacity
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Normal color
        }

    }
}
