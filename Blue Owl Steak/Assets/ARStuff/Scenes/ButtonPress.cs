using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    bool isclose;
    public GameObject linkedto;

    public void Update()
    {
        if(isclose == true && Input.GetKeyDown(KeyCode.F))
        {
            if (linkedto.transform.GetComponent<Conveyor>() != null)
            {
                linkedto.transform.GetComponent<Conveyor>().Click();
            }
            else if (linkedto.transform.GetComponent<LockedDoor>() != null)
            {
                linkedto.transform.GetComponent<LockedDoor>().Click();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isclose = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isclose = false;
        }
    }
}
