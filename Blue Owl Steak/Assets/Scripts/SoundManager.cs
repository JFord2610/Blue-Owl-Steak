using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioClip clink;
    AudioSource SoundManagerSource;

    //Enemy clips
    public static AudioClip walking = null;
    public static AudioClip splat = null;

    void Start()
    {
        instance = this;
        clink = Resources.Load<AudioClip>("Audio/part_repair");
        walking = Resources.Load<AudioClip>("Audio/enemy_walking_sound");
        splat = Resources.Load<AudioClip>("Audio/enemy_takedamage");
        SoundManagerSource = GetComponent<AudioSource>();

        EnemyStartWalkingEvent += OnEnemyStartWalking;
        EnemyStopWalkingEvent += OnEnemyStopWalking;
        EnemyTakeDamageEvent += OnEnemyTakeDamage;
    }

    private void OnDestroy()
    {
        EnemyStartWalkingEvent -= OnEnemyStartWalking;
        EnemyStopWalkingEvent -= OnEnemyStopWalking;
        EnemyTakeDamageEvent -= OnEnemyTakeDamage;
    }

    void Update()
    {
        
    }

    public void PlayRepair()
    {
        SoundManagerSource.PlayOneShot(clink);
    }

    void OnEnemyStartWalking(AudioSource source)
    {
        if (source.isPlaying && source.clip == splat)
        {
            float timeLeft = (splat.frequency * splat.samples) - source.time;
            StartCoroutine(PlayWalk(new AudioAndTime(source, timeLeft)));
            return;
        }
        source.loop = true;
        source.clip = walking;
        source.Play();
    }
    void OnEnemyStopWalking(AudioSource source)
    {
        source.loop = false;
        source.Stop();
    }
    void OnEnemyTakeDamage(AudioSource source)
    {
        source.Stop();
        source.clip = splat;
        StartCoroutine("idk", source);//it wont work if i just call play here idk its weird
    }
    IEnumerator idk(AudioSource source)
    {
        yield return null;
        source.Play();
    }

    IEnumerator PlayWalk(AudioAndTime aot)
    {
        yield return new WaitForSeconds(aot.time);

        aot.source.loop = true;
        aot.source.clip = walking;
        aot.source.Play();
    }

    public delegate void AudioHandler(AudioSource src);
    public static event AudioHandler EnemyStartWalkingEvent;
    public static event AudioHandler EnemyStopWalkingEvent;
    public static event AudioHandler EnemyTakeDamageEvent;

    public static void InvokeEnemyStartWalking(AudioSource src)
    {
        EnemyStartWalkingEvent?.Invoke(src);
    }
    public static void InvokeEnemyStopWalking(AudioSource src)
    {
        EnemyStopWalkingEvent?.Invoke(src);
    }
    public static void InvokeEnemyTakeDamage(AudioSource src)
    {
        EnemyTakeDamageEvent?.Invoke(src);
    }
}

struct AudioAndTime
{
    public AudioAndTime(AudioSource _source, float _time)
    {
        source = _source;
        time = _time;
    }
    public AudioSource source;
    public float time;
}
