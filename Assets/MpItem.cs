using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpItem : MonoBehaviour
{
    public float recoveryAmount = 10f; // �񕜗�

    // �g���K�[�Ńv���C���[�ƏՓ˂����Ƃ���MP����
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[��"Mp"�X�N���v�g���A�^�b�`����Ă��邩�m�F
        Mp playerMp = other.GetComponent<Mp>();
        if (playerMp != null)
        {
            // MP����
            playerMp.ChangeMp(recoveryAmount);
            Debug.Log("MP���񕜂���܂���: " + recoveryAmount);

            // �A�C�e��������
            Destroy(gameObject);
        }
    }
}
