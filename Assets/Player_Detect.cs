using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detect : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Rigidbody2D rb;
    public Rigidbody2D playerRb;
    public float moveSpeed;
    public int walls;
    public RaycastHit2D hit;

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

        Vector2 playerDir = playerRb.position - rb.position;
        hit = Physics2D.Raycast(rb.position, playerDir, Mathf.Infinity, walls, -Mathf.Infinity, Mathf.Infinity);
        if (hit.collider != null)
        {
            float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg - 90f;
            if (hit.collider.gameObject == player)
            {
                if (Vector2.SqrMagnitude(playerDir) < 9 + longerDetection)
                {
                    longerDetection = 16f;
                    rb.rotation = angle;
                    if (Vector2.Distance(rb.position, playerRb.position) > 3)
                    {
                        if (fireCountdown < 0.5f)
                        {
                            rb.MovePosition(rb.position + Vector2.ClampMagnitude(playerDir, 1f) * moveSpeed * Time.fixedDeltaTime);
                        }
                    }
                    if (fireCountdown <= 0f)
                    {
                        Shoot();
                        fireCountdown = 1f / fireRate;
                    }
                    fireCountdown -= Time.deltaTime;
                }

                pursuitCountdown = pursuitTime;
            }
            else
            {
                pursuitCountdown -= Time.deltaTime;
                if (pursuitCountdown > 0)
                {
                    Vector2 pastPlayerDir = playerPos2 - rb.position;
                    rb.rotation = angle;
                    rb.MovePosition(rb.position + Vector2.ClampMagnitude(pastPlayerDir, 1f) * moveSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    longerDetection = 0f;

                    //Patrol
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
}
