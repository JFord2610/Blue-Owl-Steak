using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioClip clink;
    AudioSource soundManagerSource;

    //Enemy clips
    AudioClip walking = null;
    AudioClip splat = null;

    //music
    AudioClip worldTheme1 = null;
    AudioClip worldTheme2 = null;

    float timer = 0;
    int songIndex = 0;

    void Start()
    {
        instance = this;
        clink = Resources.Load<AudioClip>("Audio/part_repair");
        walking = Resources.Load<AudioClip>("Audio/enemy_walking_sound");
        splat = Resources.Load<AudioClip>("Audio/enemy_takedamage");
        worldTheme1 = Resources.Load<AudioClip>("Audio/open_world");
        worldTheme2 = Resources.Load<AudioClip>("Audio/open_world2");
        soundManagerSource = GetComponent<AudioSource>();

        EnemyStartWalkingEvent += OnEnemyStartWalking;
        EnemyStopWalkingEvent += OnEnemyStopWalking;
        EnemyTakeDamageEvent += OnEnemyTakeDamage;

        soundManagerSource.clip = worldTheme1;
        soundManagerSource.Play();
        soundManagerSource.loop = true;
    }

    private void OnDestroy()
    {
        EnemyStartWalkingEvent -= OnEnemyStartWalking;
        EnemyStopWalkingEvent -= OnEnemyStopWalking;
        EnemyTakeDamageEvent -= OnEnemyTakeDamage;
    }
    
    public void Pause()
    {
        soundManagerSource.Pause();
    }
    public void UnPause()
    {
        soundManagerSource.Play();
    }

    public void PlayRepair()
    {
        soundManagerSource.PlayOneShot(clink);
    }
    IEnumerator PlayDelay(AudioSource source)
    {
        yield return null;
        source.Play();
    }

    #region enemies
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
        StartCoroutine("PlayDelay", source);//it wont work if i just call play here idk its weird
    }
    IEnumerator PlayWalk(AudioAndTime aot)
    {
        yield return new WaitForSeconds(aot.time);

        aot.source.loop = true;
        aot.source.clip = walking;
        aot.source.Play();
    }
    #endregion

    #region events
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
    #endregion
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
