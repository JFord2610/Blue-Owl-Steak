using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.collider.tag == "Key")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        
    }
}
