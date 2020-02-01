using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTriggerScript : MonoBehaviour
{
    GameObject bloodSplatPrefab = null;
    GameObject bloodSplat = null;
    BoxCollider col = null;

    private void Start()
    {
        bloodSplatPrefab = Resources.Load<GameObject>("Particles/BloodParticles");
        col = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            GameObject bloodSplat = Instantiate<GameObject>(bloodSplatPrefab);
            bloodSplat.transform.LookAt(other.transform.position);
            bloodSplat.transform.position = transform.position;
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage(GameManager.instance.playerController.swordDamage);
            enemyController.Knockback((transform.position - enemyController.transform.position).normalized);

            Invoke("DestroyParticles", 0.7f);

            col.enabled = false;
        }
    }

    void DestroyParticles()
    {
        Destroy(bloodSplat);
    }
}
