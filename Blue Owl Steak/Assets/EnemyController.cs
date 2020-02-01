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

    [SerializeField] Transform target = null;

    public Vector3 startPos = Vector3.zero;
    NavMeshAgent agent = null;
    Animator anim = null;
    GameObject player = null;
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameManager.instance.player;
    }
    private void Update()
    {
        anim.SetFloat("forwardMovement", Vector3.Dot(agent.velocity, transform.forward));
        anim.SetFloat("rightMovement", Vector3.Dot(agent.velocity, transform.forward));
        anim.SetFloat("distanceToPlayer", (player.transform.position - transform.position).magnitude);
        //agent.SetDestination(target.position);
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
