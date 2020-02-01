using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornColliderScript : MonoBehaviour
{
    [SerializeField] EnemyController enemyController = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.player.GetComponent<PlayerController>().TakeDamage(enemyController.damage);
            enemyController.DisableCollider();
        }
    }
}
