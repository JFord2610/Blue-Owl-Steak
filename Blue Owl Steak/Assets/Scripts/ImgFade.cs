using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgFade : MonoBehaviour
{
    [SerializeField] Image load;
    [SerializeField] float fadeSpd = 1f;
    public bool IsDown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            load.color -= new Color(0, 0, 0, Time.deltaTime * fadeSpd) ;
        }

        IsDown = true;
    }
}
