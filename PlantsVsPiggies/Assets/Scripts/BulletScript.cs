using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int pierce = 1;
    private List<int> hitBalloons = new List<int>();

    public Vector3 target;
    private Vector3 moveDirection;

    private void Start()
    {
        // Calculate the normalized direction from the current position to the target
        moveDirection = (target - transform.position).normalized;
    }

    void Update()
    {
        // Move the bullet continuously in the direction
        transform.position += moveDirection * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet collided with: " + collision.name);

        if (pierce <= 0)
            return; // Prevent further processing if bullet is already spent

        if (collision.CompareTag("Balloon"))
        {
            Debug.Log("Bullet hit a balloon!");
            if (hitBalloons.Contains(collision.GetComponent<balloonMovementScript>().BalloonID))
            {
                return;
            }

            //destroy balloon
            hitBalloons.Add(collision.GetComponent<balloonMovementScript>().BalloonID);
            collision.GetComponent<balloonMovementScript>().PopBalloon();

            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
