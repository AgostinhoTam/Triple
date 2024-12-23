using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    // �g���K�[�Ńv���C���[�ƏՓ˂����Ƃ���MP����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        // �v���C���[��"Mp"�X�N���v�g���A�^�b�`����Ă��邩�m�F
        DamageSystem playerHp = other.GetComponent<DamageSystem>();
        if (playerHp != null)
        {
            // HP����
            playerHp.TakeDamage(-playerHp.GetMaxHealth()/2);
            Debug.Log("HP���񕜂���܂���: " + -playerHp.GetMaxHealth() / 2);

            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
