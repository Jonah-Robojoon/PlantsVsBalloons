using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class plantScript : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float maxcooldown = 1f;
    private float cooldown = 1f;
    [SerializeField] private float range = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            Shoot();
        }else
        {
            cooldown -= Time.deltaTime;
        }


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Shoot()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

        GameObject furthestBalloon = null;
        float maxWaypointDistance = float.MinValue;

        foreach (var hit in hits)
        {

            if (hit.CompareTag("Balloon"))
            {
                var balloonScript = hit.GetComponent<balloonMovementScript>();
                if (balloonScript != null)
                {
                    float waypointDistance = balloonScript.myWaypointDistance;
                    if (waypointDistance > maxWaypointDistance)
                    {
                        maxWaypointDistance = waypointDistance;
                        furthestBalloon = hit.gameObject;
                    }
                }
            }
        }

        // shoot pollen at furthest balloon

        if (furthestBalloon != null)
        {
            cooldown = maxcooldown;
            GameObject pollen = Instantiate(Projectile, transform.position, Quaternion.identity);
            pollen.GetComponent<BulletScript>().target = furthestBalloon.transform.position;

            //Destroy(furthestBalloon);

        }
    }
    
}
