using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] float damage =1f;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Hit Enemy");
            DamageSystem playerDamageSystem = other.GetComponent<DamageSystem>();
            playerDamageSystem.TakeDamage(damage);
        }
     
    }
}
