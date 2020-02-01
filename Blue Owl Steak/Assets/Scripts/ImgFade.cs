using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgFade : MonoBehaviour
{
    [SerializeField] Image load;
    [SerializeField] float fadeSpd = 1f;
    public bool IsDown;
    bool fadingToBlack = false;
    bool fading = false;
    float TimePassed = 0f;
    float TimeLimit = 2f;

    public void FadeToBlack()
    {
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
        IsDown = true;
    }

    public void FadeFromBlack()
    {
        fading = true;
    }
}
