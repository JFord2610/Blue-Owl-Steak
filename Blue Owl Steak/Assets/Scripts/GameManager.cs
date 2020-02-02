using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public UIManager uiManager = null;
    public GameObject player = null;
    public PlayerController playerController = null;
    public RocketScript rocket = null;
    public ImgFade fade = null;
    public GameObject[] parts = null;

    private void Awake()
    {
        instance = this;
        player = GameObject.Find("Player");
        fade = GameObject.Find("imgFade").GetComponent<ImgFade>();
        rocket = GameObject.Find("Rocket").GetComponent<RocketScript>();
        playerController = player.GetComponent<PlayerController>();
        parts = GameObject.FindGameObjectsWithTag("Part");
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
}
