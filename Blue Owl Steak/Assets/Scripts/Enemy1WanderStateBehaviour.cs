using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1WanderStateBehaviour : StateMachineBehaviour
{
    [SerializeField] float wanderRadius = 5.0f;

    Transform playerTransform = null;
    CharacterController playerCC = null;
    PlayerController playerController = null;

    Transform enemyTransform = null;
    NavMeshAgent enemyAgent = null;
    EnemyController enemyController = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //caching
        playerTransform = GameManager.instance.player.transform;
        playerCC = playerTransform.GetComponent<CharacterController>();
        playerController = playerTransform.GetComponent<PlayerController>();

        enemyTransform = animator.transform;
        enemyAgent = animator.GetComponent<NavMeshAgent>();
        enemyController = animator.GetComponent<EnemyController>();

        //send back to start pos after de-aggro
        enemyAgent.SetDestination(enemyController.startPos);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //check if enemy is near enough to dest
        if((enemyAgent.destination - enemyTransform.position).magnitude - 0.5f <= 0.001f)
        {
            Vector3 point = (Random.insideUnitCircle * wanderRadius);
            point.z = point.y;
            point.y = 0;
            enemyAgent.SetDestination(point);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //make sure idle trigger isnt set more then once
        animator.ResetTrigger("Idle");
    }
}
