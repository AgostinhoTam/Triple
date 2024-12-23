using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] GameObject m_AttackEffect;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit Player");
            DamageSystem playerDamageSystem = other.GetComponent<DamageSystem>();
            if (playerDamageSystem != null)
            {
                playerDamageSystem.TakeDamage(damage);
                // �G�t�F�N�g�Đ�
                PlayHitEffect(other.ClosestPoint(transform.position));
            }
        }
        Debug.Log("HitTirgger");

    }
    void PlayHitEffect(Vector3 Position)
    {
        Instantiate(m_AttackEffect, Position, Quaternion.identity);
    }
}
