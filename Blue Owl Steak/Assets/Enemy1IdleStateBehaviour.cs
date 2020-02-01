using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1IdleStateBehaviour : StateMachineBehaviour
{
    Transform playerTransform = null;
    CharacterController playerCC = null;
    PlayerController playerController = null;

    Transform enemyTransform = null;
    EnemyController enemyController = null;
    UnityEngine.AI.NavMeshAgent enemyAgent = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameManager.instance.player.transform;
        playerCC = playerTransform.GetComponent<CharacterController>();
        playerController = playerTransform.GetComponent<PlayerController>();

        enemyTransform = animator.transform;
        enemyController = animator.GetComponent<EnemyController>();
        enemyAgent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    [SerializeField] float timer = 2.0f;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
            animator.SetTrigger("Wander");
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Wander");
    }
}
