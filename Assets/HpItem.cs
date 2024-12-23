using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    // �g���K�[�Ńv���C���[�ƏՓ˂����Ƃ���MP����
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[��"Mp"�X�N���v�g���A�^�b�`����Ă��邩�m�F
        DamageSystem playerHp = other.GetComponent<DamageSystem>();
        if (playerHp != null)
        {
            // MP����
            playerHp.TakeDamage(-playerHp.GetMaxHealth()/2);
            Debug.Log("MP���񕜂���܂���: " + -playerHp.GetMaxHealth() / 2);

            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
