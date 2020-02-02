using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    bool isclose;
    public List<GameObject> linkedto = new List<GameObject>();

    public void Update()
    {
        if (isclose == true && Input.GetKeyDown(KeyCode.F))
        {
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
