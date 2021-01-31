using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    AudioSource bullet;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject mainCamera;

    public float bulletForce = 20f;
    public float ammo = 3f;

    private void Start()
    {
        bullet = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ammo > 0f)
        {
            Shoot();
            ammo -= 1;
            bullet.Play();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce((firePoint.up * bulletForce), ForceMode2D.Impulse);
    }
}
