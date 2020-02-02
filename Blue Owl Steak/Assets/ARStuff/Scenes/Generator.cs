using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    bool isclose;
    public List<GameObject> linkedto = new List<GameObject>();
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
            for (int i = 0; i < linkedto.Count; i++)
            {
                if (linkedto[i].transform.GetComponent<Conveyor>() != null)
                {
                    linkedto[i].transform.GetComponent<Conveyor>().Click();
                }
                else if (linkedto[i].transform.GetComponent<LockedDoor>() != null)
                {
                    linkedto[i].transform.GetComponent<LockedDoor>().Click();
                }
            }
            Destroy(other.gameObject);
        }
    }
}
