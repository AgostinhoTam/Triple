using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Mp mpManager;
    DamageSystem m_DamageSystem;
    // �U���ݒ�p�N���X
    [System.Serializable]
    public class AttackSetting
    {
        public GameObject attackArea; // �U���͈̓I�u�W�F�N�g
        public KeyCode activationKey; // �U���𔭓�����L�[
        public float attackAreaTime; // �U���͈͂̎�������
        public float gaugeConsumption; // �Q�[�W�̖��b�����
        public bool isActive = false; // �U���͈͂��A�N�e�B�u�����ǂ��� // �U���͈͂��A�N�e�B�u�����ǂ���
    }

    public List<AttackSetting> attackSettings = new List<AttackSetting>(); // �U���ݒ�̃��X�g

    // Start is called before the first frame update
    void Start()
    {
        m_DamageSystem = GetComponent<DamageSystem>();
        // �U���͈͂�������ԂŔ�A�N�e�B�u�ɐݒ�
        foreach (var attack in attackSettings)
        {
            if (attack.attackArea != null)
                attack.attackArea.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttacks();
        if(m_DamageSystem.GetHealth() <= 0) {Destroy(gameObject); return; }
    }

    // �U������
    void HandleAttacks()
    {
        foreach (var attack in attackSettings)
        {
            // �U���͈͂��A�N�e�B�u��ԂȂ���͂𖳎�
            if (attack.isActive)
                continue;

            // �w��L�[�������ꂽ�ꍇ�ɍU���������J�n
            if (Input.GetKeyDown(attack.activationKey) && attack.attackArea != null)
            {
                StartCoroutine(ActivateAttackArea(attack));
                mpManager.ChangeMp(-attack.gaugeConsumption);
            }
        }
    }

    // �U���͈͂���莞�ԃA�N�e�B�u��
    private IEnumerator ActivateAttackArea(AttackSetting attack)
    {
        attack.isActive = true; // �U���͈͂��A�N�e�B�u����Ԃɐݒ�
        attack.attackArea.SetActive(true); // �U���͈͂��A�N�e�B�u��

        yield return new WaitForSeconds(attack.attackAreaTime); // �������ԑҋ@

        attack.attackArea.SetActive(false); // �U���͈͂��A�N�e�B�u��
        attack.isActive = false; // �U���͈͂̃A�N�e�B�u��Ԃ�����
    }
}
