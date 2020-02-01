using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioClip clink;
    AudioSource SoundManagerSource;
    void Start()
    {
        instance = this;
        clink = Resources.Load<AudioClip>("Audio/part_repair");
        SoundManagerSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayRepair()
    {
        SoundManagerSource.PlayOneShot(clink);
    }
}
