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

    private void Start()
    {
        redFade = GameObject.Find("RedFade").GetComponent<Image>();
        deathScreen = GameObject.Find("DeathScreen").GetComponent<Image>();

        PlayerController.DeathEvent += () => { StartCoroutine("DeathRoutine");  };
    }

    IEnumerator DeathRoutine()
    {
        while(redFade.color.a <= 0.7f)
        {
            redFade.color = redFade.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(pauseBetweenRedAndDeathScreen);
        while(deathScreen.color.a <= 1)
        {
            deathScreen.color = deathScreen.color += new Color(0, 0, 0, Time.deltaTime * fadeSpeed* 0.2f);
            yield return new WaitForEndOfFrame();
        }
        Invoke("ReloadScene", 4.0f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("JohnScene");
    }
}
