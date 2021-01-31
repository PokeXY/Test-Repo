using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class door : MonoBehaviour
{
    AudioSource door2;
    public Camera_Follow follow;
    // Start is called before the first frame update
    void Start()
    {
        door2 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (follow.keycards >= 2f)
            {
                Destroy(gameObject);
            }
            else
            {
                follow.keycardHint();
            }
        }
    }
}
