using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject player = null;
    public PlayerController playerController = null;
    public RocketScript rocket = null;
    public ImgFade fade = null;

    private void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
        fade = GameObject.Find("imgFade").GetComponent<ImgFade>();
        rocket = GameObject.Find("Rocket").GetComponent<RocketScript>();
        playerController = player.GetComponent<PlayerController>();
    }
}
