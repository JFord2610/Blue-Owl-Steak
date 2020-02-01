using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target = null;

    Vector3 startPos = Vector3.zero;
    NavMeshAgent agent = null;
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {

        //agent.SetDestination(target.position);
    }
}
