using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] float damage = 1f;

    private void OnTriggerStay(Collider other)
    {

        Debug.Log(other.tag);
        Debug.Log("HitTirgger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }
}
