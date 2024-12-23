using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] float damage =1f;
    [SerializeField] GameObject hitEffectPrefab; // �p�[�e�B�N���V�X�e����Prefab
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Hit Enemy");

            // �_���[�W����
            DamageSystem playerDamageSystem = other.GetComponent<DamageSystem>();
            if (playerDamageSystem != null)
            {
                playerDamageSystem.TakeDamage(damage);
            }

            // �G�t�F�N�g�Đ�
            PlayHitEffect(other.ClosestPoint(transform.position));
        }
    }

    private void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            // �p�[�e�B�N���V�X�e�����C���X�^���X�����čĐ�
            Instantiate(hitEffectPrefab, position, Quaternion.identity);
        }
    }
}
