using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDamage : MonoBehaviour
{
    public bool isActive = false;
    public bool hasStarted = false;

    public PlayerController target;

    public float damage, delay;
    // Update is called once per frame
    void Update()
    {
        if (isActive == true && hasStarted == false)
        {
            hasStarted = true;
            StartCoroutine(DPS());
        }
    }

    IEnumerator DPS()
    {
        target.TakeDamage(damage);

        yield return new WaitForSeconds(delay);

        if (isActive == true){
            StartCoroutine(DPS());
        }
        else
        {
            hasStarted = false;
        }
    }

    void OnTriggerEnter(Collider col)
        {
            if (col.GetComponent<PlayerController>() != null)
            {
                target = col.GetComponent<PlayerController>();
                isActive = true;
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.GetComponent<PlayerController>() != null)
            {
                isActive = false;
            }
        }
    }
