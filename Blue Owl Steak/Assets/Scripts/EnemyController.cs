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
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameManager.instance.player;
        Health = MaxHealth;
    }
    private void Update()
    {
        anim.SetFloat("forwardMovement", Vector3.Dot(agent.velocity, transform.forward));
        anim.SetFloat("rightMovement", Vector3.Dot(agent.velocity, transform.forward));
        anim.SetFloat("distanceToPlayer", (player.transform.position - transform.position).magnitude);
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    public void Knockback(Vector3 _dir)
    {
        StartCoroutine("KnockbackRoutine", _dir);
    }
    IEnumerator KnockbackRoutine(Vector3 _dir)
    {
        for (int i = 0; i < 30; i++)
        {
            transform.position = Vector3.Lerp(transform.position, _dir * 5.0f, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    void Kill()
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
        if(other.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
