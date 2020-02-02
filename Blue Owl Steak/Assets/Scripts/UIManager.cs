using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //variables
    [SerializeField] float fadeSpeed = 1.0f;
    [SerializeField] float pauseBetweenRedAndDeathScreen = 1.0f;

    Image redFade = null;
    Image deathScreen = null;
    Image winScreen = null;
    GameObject fToInteract = null;
    public bool canInteract = false;

    private void Start()
    {
        redFade = GameObject.Find("RedFade").GetComponent<Image>();
        deathScreen = GameObject.Find("DeathScreen").GetComponent<Image>();
        winScreen = GameObject.Find("WinScreen").GetComponent<Image>();
        fToInteract = GameObject.Find("FToInteract");

        PlayerController.DeathEvent += () => { StartCoroutine("DeathRoutine"); };

        EventManager.GameWinEvent += OnWin;
    }

    private void OnDisable()
    {
        EventManager.GameWinEvent -= OnWin;
    }

    IEnumerator DeathRoutine()
    {
        while (redFade.color.a <= 0.7f)
        {
            redFade.color = redFade.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(pauseBetweenRedAndDeathScreen);
        while (deathScreen.color.a <= 1)
        {
            deathScreen.color = deathScreen.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed * 0.2f);
            yield return new WaitForEndOfFrame();
        }
        Invoke("ReloadScene", 4.0f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Level");
        Cursor.visible = false;
    }

    void OnWin()
    {
        SoundManager.instance.Pause();
        StartCoroutine("FadeWinIn");
        Invoke("ActivateButtons", fadeSpeed);
        Cursor.visible = true;
    }

    IEnumerator FadeWinIn()
    {
        while (winScreen.color.a <= 1.0f)
        {
            winScreen.color = winScreen.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
    void ActivateButtons()
    {
        winScreen.transform.GetChild(0).gameObject.SetActive(true);
        winScreen.transform.GetChild(1).gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
