using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgFade : MonoBehaviour
{
    [SerializeField] Image load;
    public float fadeSpd = 1f;
    public float TimeLimit = 7f;
    bool fadingToBlack = false;
    bool fading = false;

    public float totalFadeTime
    {
        get
        {
            return TimeLimit + (fadeSpd * 2);
        }
    }

    public void FadeToBlack()
    {
        GameManager.instance.playerController.disabled = true;
        fadingToBlack = true;
        fading = true;
    }

    private void Update()
    {
        if (fading)
        {
            load.color += new Color(0, 0, 0, (fadingToBlack ? 1 : -1) * Time.deltaTime * fadeSpd);
            if (load.color.a >= 1)
            {
                fadingToBlack = false;
                fading = false;
                Invoke("FadeFromBlack", TimeLimit);
                SoundManager.instance.PlayRepair();
            }
            if (load.color.a <= 0)
            {
                fading = false;
            }
        }

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    FadeToBlack();
        //}
    }

    public void FadeFromBlack()
    {
        GameManager.instance.playerController.disabled = false;
        fading = true;
    }
}
