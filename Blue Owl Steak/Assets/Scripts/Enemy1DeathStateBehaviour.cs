using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1DeathStateBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyController>().DisableCollider();
        animator.ResetTrigger("Damaged");
        animator.GetComponent<NavMeshAgent>().SetDestination(animator.transform.position);
    }
}
