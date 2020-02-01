using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject player = null;
    public PlayerController playerController = null;

    private void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
}
