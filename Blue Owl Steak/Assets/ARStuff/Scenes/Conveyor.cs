using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Transform endOfConveyor;
    public float speed;
    public bool ison;

    private void OnTriggerStay(Collider other)
    {
        if(ison == true)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position,endOfConveyor.position,speed* Time.deltaTime);
        }
    }

    public void Click()
    {
        ison = true;
    }
}
