using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [SerializeField] float playerCloseEnough = 5.0f; //I couldnt think of a good name sue me

    GameManager gameManager = null;
    GameObject player = null;
    PlayerController playerController = null;
    public int partCount = 0;

    private void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.player;
        playerController = gameManager.playerController;
        EventManager.PlayerDroppedItemEvent += OnPlayerDroppedItem;
    }

    void OnPlayerDroppedItem()
    {
        if ((player.transform.position - transform.position).magnitude <= playerCloseEnough && playerController?.objectBeingHeld.tag == "Part")
        {
            playerController.disabled = true;
            gameManager.fade.FadeToBlack();
            Invoke("EnablePlayer", gameManager.fade.totalFadeTime);
            Invoke("DestroyPart", gameManager.fade.fadeSpd * 2);
        }
    }
    
    void DestroyPart()
    {
        Destroy(playerController.objectBeingHeld);
    }

    void EnablePlayer()
    {
        playerController.disabled = false;
        partCount++;
        Debug.Log($"Part Count: {partCount}");
        if (partCount == 3)
        {
            EventManager.InvokeGameWinEvent();
        }
    }
}
