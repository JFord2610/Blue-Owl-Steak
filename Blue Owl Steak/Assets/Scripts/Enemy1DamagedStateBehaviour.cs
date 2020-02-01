using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1DamagedStateBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyController>().DisableCollider();
    }
}
