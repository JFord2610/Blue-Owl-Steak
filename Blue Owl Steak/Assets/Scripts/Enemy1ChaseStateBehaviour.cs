using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1ChaseStateBehaviour : StateMachineBehaviour
{
    Transform playerTransform = null;
    CharacterController playerCC = null;
    PlayerController playerController = null;

    Transform enemyTransform = null;
    EnemyController enemyController = null;
    NavMeshAgent enemyAgent = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameManager.instance.player.transform;
        playerCC = playerTransform.GetComponent<CharacterController>();
        playerController = playerTransform.GetComponent<PlayerController>();

        enemyTransform = animator.transform;
        enemyController = animator.GetComponent<EnemyController>();
        enemyAgent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent.SetDestination(playerTransform.position);
        if((playerTransform.position - enemyTransform.position).magnitude <= 1.25f)
        {
            enemyAgent.SetDestination(enemyTransform.position);
            enemyTransform.LookAt(playerTransform);
            animator.SetTrigger("Attack");
        }
        else if((playerTransform.position - enemyTransform.position).magnitude > 20.0f)
        {
            enemyAgent.SetDestination(enemyTransform.position);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
