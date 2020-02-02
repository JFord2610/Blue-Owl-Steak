using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    /* collection point for parts
     * fade out when collecting a part, switch to new model during blackout
     * 3 parts, 3 stages
     * after 3 are done, win game
     */


    GameObject Phase1 = null;
    GameObject Phase2 = null;
    GameObject Phase3 = null;

    [SerializeField] float gatherDistance = 5.0f;
    uint partsGathered = 0;

    private void Start()
    {
        Phase1 = transform.Find("RocketPhaseOne").gameObject;
        Phase2 = transform.Find("RocketPhaseTwo").gameObject;
        Phase3 = transform.Find("RocketPhaseThree").gameObject;
    }

    private void Update()
    {
        //check if parts are near the rockets, if they are close enough, start the fade out and model switch
        foreach (GameObject part in GameManager.instance.parts)
        {
            if (part == null) continue;
            float dist = (new Vector3(part.transform.position.x, 0, part.transform.position.z) - transform.position).magnitude;
            if (dist <= gatherDistance)
            {
                //init fade
                GameManager.instance.fade.FadeToBlack();
                Invoke("UpgradeShip", 4.0f);
                Destroy(part); // the cause of th eneed for a null check
            }
        }
    }

    private void UpgradeShip()
    {
        partsGathered++;
        switch (partsGathered)
        {
            case 1:
                Phase1.SetActive(false);
                Phase2.SetActive(true);
                break;
            case 2:
                Phase2.SetActive(false);
                Phase3.SetActive(true);
                break;
            case 3:
                EventManager.InvokeGameWinEvent();
                GameManager.instance.fade.FadeFromBlack();
                //win game
                break;
        }
    }
}
