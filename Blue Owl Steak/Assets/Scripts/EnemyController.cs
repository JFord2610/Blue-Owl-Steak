using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    float _health = 0;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value >= MaxHealth)
                _health = MaxHealth;
            else if (value <= 0)
                Kill();
            else
            {
                if (_health > value)
                    anim.SetTrigger("Damaged");
                _health = value;
            }
        }
    }
    [SerializeField] float MaxHealth = 10.0f;
    public float damage = 10.0f;

    public Vector3 startPos = Vector3.zero;
    NavMeshAgent agent = null;
    Animator anim = null;
    GameObject player = null;
    [SerializeField] BoxCollider col = null;
    AudioSource source = null;

    bool dead = false;
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameManager.instance.player;
        source = GetComponent<AudioSource>();
        Health = MaxHealth;
    }
    private void Update()
    {
        if (dead) return;
        if (!GameManager.instance.playerController.disabled)
        {
            anim.SetFloat("forwardMovement", Vector3.Dot(agent.velocity, transform.forward));
            anim.SetFloat("rightMovement", Vector3.Dot(agent.velocity, transform.forward));
            anim.SetFloat("distanceToPlayer", (player.transform.position - transform.position).magnitude);
        }
        else
        {
            anim.SetFloat("forwardMovement", 0);
            anim.SetFloat("rightMovement", 0);
            anim.SetFloat("distanceToPlayer", 100);
        }
        //if(agent.velocity.magnitude >= 0.001f)
        //{
        //    source.clip = SoundManager.walking;
        //    source.loop = true;
        //}
        //else
        //{
        //    source.loop = false;
        //    source.Stop();
        //}
    }
    public void TakeDamage(float damage)
    {
        if (dead) return;
        anim.SetTrigger("Damaged");
        SoundManager.InvokeEnemyTakeDamage(source);
        Health -= damage;
    }

    void Kill()
    {
        anim.SetTrigger("Death");
        dead = true;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void EnableCollider()
    {
        col.enabled = false;
    }

    public void DisableCollider()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
