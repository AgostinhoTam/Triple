using UnityEngine;
using UnityEngine.UI;

public class Mp : MonoBehaviour
{
    public float maxMp = 100f; // �ő�MP
    private float curMp; // ���݂�MP
    private float displayedMp; // �\���p��MP�i�X���[�Y�ɍX�V�����l�j

    public Slider mpBarSlider; // MP�o�[�̃X���C�_�[
    public float smoothSpeed = 5f; // �X���[�Y�ȕω����x

    void Start()
    {
        curMp = maxMp; // MP���ő�l�ŏ�����
        displayedMp = maxMp; // �\���pMP���ő�l�ŏ�����
        UpdateMpBar(); // MP�o�[��������
    }

    void Update()
    {
        // �\���p��MP���X���[�Y�Ɍ��݂�MP�ɋ߂Â���
        displayedMp = Mathf.Lerp(displayedMp, curMp, Time.deltaTime * smoothSpeed);
        UpdateMpBar(); // MP�o�[���X�V
    }

    // MP��ύX���郁�\�b�h
    public void ChangeMp(float amount)
    {
        curMp = Mathf.Clamp(curMp + amount, 0, maxMp); // MP�͈̔͂𐧌�
    }

    // ���݂�MP���擾���郁�\�b�h
    public float GetCurrentMp()
    {
        return curMp;
    }

    // MP�o�[�̃X���C�_�[���X�V
    private void UpdateMpBar()
    {
        if (mpBarSlider != null)
        {
            mpBarSlider.value = displayedMp / maxMp; // �X���C�_�[�̒l���X�V
        }
    }
}
