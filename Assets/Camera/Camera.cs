using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float smoothSpeed = 0.125f; // カメラ移動のスムーズ速度

    private bool isCameraLocked = false; // カメラが固定されているかどうか
    private Vector3 lockedPosition; // 固定する位置

    private string collisionStatus = "No Collision"; // 衝突状態の文字列（デバッグ用）

    // ロックエリアを管理するクラス
    [System.Serializable]
    public class LockArea
    {
        public GameObject lockAreaObject; // ロックエリアのオブジェクト
        public GameObject lockObject; // カメラを固定する対象オブジェクト
        public bool isCleared = false; // そのエリアがクリアされたかどうか
    }

    public List<LockArea> lockAreas = new List<LockArea>(); // ロックエリアのリスト

    void Start()
    {
        // 必要に応じて初期設定を行う
    }

    void Update()
    {
        CameraMove(); // カメラ移動処理を呼び出す

        // デバッグ用：1キーを押してエリアをクリア
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClearLockArea(0); // リストの0番目のエリアをクリア
        }
    }

    void CameraMove()
    {
        Vector3 targetPosition; // カメラの目標位置

        if (isCameraLocked)
        {
            // カメラが固定されている場合、固定位置に移動
            targetPosition = new Vector3(
                lockedPosition.x ,
                lockedPosition.y ,
                lockedPosition.z 
            );
        }
        else if (player != null)
        {
            // プレイヤーを追従する場合 (X軸のみ移動)
            targetPosition = new Vector3(
                player.position.x,
                transform.position.y,
                transform.position.z
            );
        }
        else
        {
            return; // プレイヤーが設定されていない場合は何もしない
        }

        Vector3 smoothedPosition;
        if (!isCameraLocked)
        {
            // 現在の位置から目標位置に向かってスムーズに移動
            smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
        else
        {
            smoothedPosition = Vector3.Lerp(transform.position, targetPosition, 0.01f);
        }
        transform.position = smoothedPosition; // 新しい位置をカメラに適用
    }

    // 特定のフィールドに入った時の処理
    private void OnTriggerEnter(Collider other)
    {
        foreach (LockArea lockArea in lockAreas)
        {
            if (other.gameObject == lockArea.lockAreaObject && !lockArea.isCleared)
            {
                collisionStatus = "Collision with CameraField"; // 衝突状態を更新
                isCameraLocked = true; // カメラを固定モードに変更
                lockedPosition = lockArea.lockObject.transform.position; // lockObjectの位置を固定位置として設定
                break;
            }
        }
    }

    // カメラ固定を解除する処理
    public void UnlockCamera()
    {
        isCameraLocked = false; // 固定モードを解除
        collisionStatus = "No Collision"; // 衝突状態をリセット
    }

    // 特定のロックエリアをクリアする処理
    public void ClearLockArea(int index)
    {
        if (index >= 0 && index < lockAreas.Count)
        {
            LockArea lockArea = lockAreas[index];
            if (!lockArea.isCleared)
            {
                lockArea.isCleared = true; // エリアをクリア状態に設定
                UnlockCamera(); // カメラ固定を解除
            }
        }
    }

    // 衝突状態とロックエリアのクリア状態を画面に表示 (デバッグ用)
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "Collision Status: " + collisionStatus);

        for (int i = 0; i < lockAreas.Count; i++)
        {
            GUI.Label(new Rect(10, 30 + i * 20, 300, 20), $"LockArea {i} Cleared: {lockAreas[i].isCleared}");
        }
    }
}
