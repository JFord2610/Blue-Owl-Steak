using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    public AudioSource SoundManagerSource;
    public AudioClip hover;
    public AudioClip click;

public void Start()
    
    click = Resources.Load<AudioClip>("click");
    hover = Resources.Load<AudioClip>("hover");
    SoundManagerSource = Getcomponent<AudioSource>();

    // Start is called before the first frame update

    public void Onhover()
    {
        SoundManagerSource.PlayOneShot(hover);
    }

    // Update is called once per frame
    public void Onclick()
    {
        SoundManagerSource.PlayOneShot(click);
    }
}