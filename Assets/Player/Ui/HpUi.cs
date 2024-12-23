using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    public DamageSystem hp; // HP���Ǘ�����X�N���v�g
    private float displayedHp; // �\���p��HP�i�X���[�Y�ɍX�V�����l�j

    public Slider hpBarSlider; // HP�o�[�̃X���C�_�[
    public float smoothSpeed = 8f; // �X���[�Y�ȕω����x

    void Start()
    {
        displayedHp = hp.GetMaxHealth(); // �\���pHP�����݂�HP�ŏ�����
        UpdateHpBar(); // HP�o�[��������
    }

    void Update()
    {
        // �\���p��HP���X���[�Y�Ɍ��݂�HP�ɋ߂Â���
        displayedHp = Mathf.MoveTowards(displayedHp, hp.GetHealth(), smoothSpeed * Time.deltaTime);
        UpdateHpBar(); // HP�o�[���X�V
    }

    // HP�o�[�̃X���C�_�[���X�V
    private void UpdateHpBar()
    {
        if (hpBarSlider != null)
        {
            hpBarSlider.value = displayedHp / hp.GetMaxHealth(); // �X���C�_�[�̒l���X�V
        }
    }
    public void ZeroHp()
    {
        hpBarSlider.value = 0.0f;
    }
}
