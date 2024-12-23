using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // �v���C���[��Transform
    public float smoothSpeed = 0.125f; // �J�����ړ��̃X���[�Y���x

    private bool isCameraLocked = false; // �J�������Œ肳��Ă��邩�ǂ���
    private Vector3 lockedPosition; // �Œ肷��ʒu

    private string collisionStatus = "No Collision"; // �Փˏ�Ԃ̕�����i�f�o�b�O�p�j

    // ���b�N�G���A���Ǘ�����N���X
    [System.Serializable]
    public class LockArea
    {
        public GameObject lockAreaObject; // ���b�N�G���A�̃I�u�W�F�N�g
        public GameObject lockObject; // �J�������Œ肷��ΏۃI�u�W�F�N�g
        public bool isCleared = false; // ���̃G���A���N���A���ꂽ���ǂ���
    }

    public List<LockArea> lockAreas = new List<LockArea>(); // ���b�N�G���A�̃��X�g

    void Start()
    {
        // �K�v�ɉ����ď����ݒ���s��
    }

    void Update()
    {
        CameraMove(); // �J�����ړ��������Ăяo��

        // �f�o�b�O�p�F1�L�[�������ăG���A���N���A
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClearLockArea(0); // ���X�g��0�Ԗڂ̃G���A���N���A
        }
    }

    void CameraMove()
    {
        Vector3 targetPosition; // �J�����̖ڕW�ʒu

        if (isCameraLocked)
        {
            // �J�������Œ肳��Ă���ꍇ�A�Œ�ʒu�Ɉړ�
            targetPosition = new Vector3(
                lockedPosition.x ,
                lockedPosition.y ,
                lockedPosition.z 
            );
        }
        else if (player != null)
        {
            // �v���C���[��Ǐ]����ꍇ (X���݈̂ړ�)
            targetPosition = new Vector3(
                player.position.x,
                transform.position.y,
                transform.position.z
            );
        }
        else
        {
            return; // �v���C���[���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        }

        Vector3 smoothedPosition;
        if (!isCameraLocked)
        {
            // ���݂̈ʒu����ڕW�ʒu�Ɍ������ăX���[�Y�Ɉړ�
            smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
        else
        {
            smoothedPosition = Vector3.Lerp(transform.position, targetPosition, 0.01f);
        }
        transform.position = smoothedPosition; // �V�����ʒu���J�����ɓK�p
    }

    // ����̃t�B�[���h�ɓ��������̏���
    private void OnTriggerEnter(Collider other)
    {
        foreach (LockArea lockArea in lockAreas)
        {
            if (other.gameObject == lockArea.lockAreaObject && !lockArea.isCleared)
            {
                collisionStatus = "Collision with CameraField"; // �Փˏ�Ԃ��X�V
                isCameraLocked = true; // �J�������Œ胂�[�h�ɕύX
                lockedPosition = lockArea.lockObject.transform.position; // lockObject�̈ʒu���Œ�ʒu�Ƃ��Đݒ�
                break;
            }
        }
    }

    // �J�����Œ���������鏈��
    public void UnlockCamera()
    {
        isCameraLocked = false; // �Œ胂�[�h������
        collisionStatus = "No Collision"; // �Փˏ�Ԃ����Z�b�g
    }

    // ����̃��b�N�G���A���N���A���鏈��
    public void ClearLockArea(int index)
    {
        if (index >= 0 && index < lockAreas.Count)
        {
            LockArea lockArea = lockAreas[index];
            if (!lockArea.isCleared)
            {
                lockArea.isCleared = true; // �G���A���N���A��Ԃɐݒ�
                UnlockCamera(); // �J�����Œ������
            }
        }
    }

    // �Փˏ�Ԃƃ��b�N�G���A�̃N���A��Ԃ���ʂɕ\�� (�f�o�b�O�p)
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Collision Status: " + collisionStatus);

        for (int i = 0; i < lockAreas.Count; i++)
        {
            GUI.Label(new Rect(10, 30 + i * 20, 300, 20), $"LockArea {i} Cleared: {lockAreas[i].isCleared}");
        }
    }
}
