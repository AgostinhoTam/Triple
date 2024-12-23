using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] float damage =1f;
    [SerializeField] GameObject hitEffectPrefab; // パーティクルシステムのPrefab
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Hit Enemy");

            // ダメージ処理
            DamageSystem playerDamageSystem = other.GetComponent<DamageSystem>();
            if (playerDamageSystem != null)
            {
                playerDamageSystem.TakeDamage(damage);
            }

            // エフェクト再生
            PlayHitEffect(other.ClosestPoint(transform.position));
        }
    }

    private void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            // パーティクルシステムをインスタンス化して再生
            Instantiate(hitEffectPrefab, position, Quaternion.identity);
        }
    }
}
