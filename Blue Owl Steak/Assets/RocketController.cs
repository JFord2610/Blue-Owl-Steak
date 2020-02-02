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
    Transform spawnPoint = null;

    [SerializeField] float gatherDistance = 5.0f;
    uint partsGathered = 2;

    private void Start()
    {
        Phase1 = transform.Find("RocketPhaseOne").gameObject;
        Phase2 = transform.Find("RocketPhaseTwo").gameObject;
        Phase3 = transform.Find("RocketPhaseThree").gameObject;
        spawnPoint = GameObject.Find("StartPos").transform;
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

                //drop part then delete it
                GameManager.instance.playerController.DropHeldObject();
                Destroy(part);
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
                //win game
                break;
        }

        //move player and heal player
        GameManager.instance.playerController.Health = GameManager.instance.playerController.MaxHealth;
        GameManager.instance.player.transform.position = spawnPoint.position;
    }
}
