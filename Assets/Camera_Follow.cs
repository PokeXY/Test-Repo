using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Camera_Follow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public GameObject ammo1;
    public GameObject ammo2;
    public GameObject ammo3;
    public GameObject keycard1;
    public GameObject keycard2;
    public float ammos;
    public float keycards;



    void Start()
    {
        ammos = 3f;
        keycards = 0f;
        keycard1.SetActive(false);
        keycard2.SetActive(false);
        ammo3.SetActive(true);
        ammo2.SetActive(true);
        ammo1.SetActive(true);
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }

    public void ammoUpdate()
    {
        if (keycards == 3f)
        {
            ammo3.SetActive(false);
        }
        else if (keycards == 2f)
        {
            ammo2.SetActive(false);
        }
        else if (keycards == 1f)
        {
            ammo1.SetActive(false);
        }

        ammos -= 1;
    }

    public void keycardUpdate()
    {
        if (keycards == 0f)
        {
            keycard2.SetActive(true);
        }
        else if (keycards == 1f)
        {
            keycard1.SetActive(true);
        }  
        keycards += 1f;
    }
}
