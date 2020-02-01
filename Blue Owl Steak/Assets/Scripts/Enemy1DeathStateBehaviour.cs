using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1DeathStateBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyController>().DisableCollider();
    }
}
