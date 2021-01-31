using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{

    public GameObject mainCamera;
    public Camera_Follow follow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        follow.keycardUpdate();
        Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
        {
            
        }


    }
}
