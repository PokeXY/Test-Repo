using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detect : MonoBehaviour
{
    AudioSource enemyBullet;
    public GameObject player;
    public GameObject enemy;
    public Rigidbody2D rb;
    public Rigidbody2D playerRb;
    public float moveSpeed;
    public int walls;
    public RaycastHit2D hit;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public Transform start;
    public Transform target;
    public Vector2 targetV2;
    public Vector2 playerDir;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float detectionTime = 1f;
    public float detectionCountdown = 0f;
    public float pursuitCountdown = 0f;
    public float pursuitTime = 3f;
    public float playerMovementCountdown = 0f;
    public float playerMovementTrack = 2f;
    public float longerDetection = 0f;

    public Vector2 playerPos1;
    public Vector2 playerPos2;

    void Start()
    {
        enemyBullet = GetComponent<AudioSource>();
        target = start;
        targetV2 = new Vector2(start.position.x, start.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementCountdown < 0f)
        {
            playerPos2 = playerPos1;
            playerPos1 = playerRb.position;
            playerMovementCountdown = playerMovementTrack;
        }
        playerMovementCountdown -= Time.deltaTime;

        playerDir = playerRb.position - rb.position;
        hit = Physics2D.Raycast(rb.position, playerDir, Mathf.Infinity, walls, -Mathf.Infinity, Mathf.Infinity);
        if (hit.collider != null)
        {
            float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg - 90f;
            if (hit.collider.gameObject == player && playerDir.magnitude < 2f + longerDetection)
            {
                longerDetection = 3f;
                rb.rotation = angle;
                if (Vector2.Distance(rb.position, playerRb.position) > 3)
                {
                    rb.MovePosition(rb.position + Vector2.ClampMagnitude(playerDir, 1f) * moveSpeed * Time.fixedDeltaTime);
                }
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                    enemyBullet.Play();
                }
                fireCountdown -= Time.deltaTime;

                pursuitCountdown = pursuitTime;
            }
            else
            {
                pursuitCountdown -= Time.deltaTime;
                if (pursuitCountdown > 0f)
                {
                    Vector2 pastPlayerDir = playerPos2 - rb.position;
                    rb.rotation = angle;
                    rb.MovePosition(rb.position + Vector2.ClampMagnitude(pastPlayerDir, 1f) * moveSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    longerDetection = 0f;

                    //Patrol
                    Vector2 patrolDir = targetV2 - rb.position;
                    rb.MovePosition(rb.position + Vector2.ClampMagnitude(patrolDir, 1f) * moveSpeed * Time.fixedDeltaTime);

                    if (Vector2.Distance(rb.position, targetV2) < 0.05f)
                    {
                        GetNextWaypoint();
                    }

                }
            }
        }
    }



    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce((firePoint.up * bulletForce), ForceMode2D.Impulse);
    }

    void GetNextWaypoint()
    {
        if (target == point1)
        {
            rb.rotation = 90f;
            target = point2;
            targetV2 = new Vector2(point2.position.x, point2.position.y);
        }
        else if (target == point2)
        {
            rb.rotation = 0f;
            target = point3;
            targetV2 = new Vector2(point3.position.x, point3.position.y);
        }
        else if (target == point3)
        {
            rb.rotation = 270f;
            target = point4;
            targetV2 = new Vector2(point4.position.x, point4.position.y);
        }
        else if (target == point4)
        {
            rb.rotation = 180f;
            target = point1;
            targetV2 = new Vector2(point1.position.x, point1.position.y);
        }
    }
}
