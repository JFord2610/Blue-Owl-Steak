using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    bool isclose;
    public GameObject linkedto;
    GameObject batteryalt;

    private void Start()
    {
        batteryalt = transform.GetChild(0).gameObject;
        batteryalt.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery")
        {
            batteryalt.SetActive(true);
            if (linkedto.transform.GetComponent<Conveyor>() != null)
            {
                linkedto.transform.GetComponent<Conveyor>().Click();
            }
            else if (linkedto.transform.GetComponent<LockedDoor>() != null)
            {
                linkedto.transform.GetComponent<LockedDoor>().Click();
            }
            Destroy(other.gameObject);
        }
    }
}
