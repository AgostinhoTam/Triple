using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed; // 移動速度
    public float dodgeTime; // 回避持続時間

    private float originalSpeed; // 元の移動速度を保存

    float hAxis; // 横方向の入力
    float vAxis; // 縦方向の入力

    Vector3 moveVec; // 移動ベクトル
    Vector3 dodgeVec; // 回避ベクトル

    bool wDown; // 走る状態かどうか
    bool isDodge; // 回避中かどうか


    void Start()
    {
        originalSpeed = speed; // ゲーム開始時に元の速度を保存
    }

    void Update()
    {
        GetInput(); // 入力を取得
        Move(); // 移動処理
        Dodge(); // 回避処理
        Turn(); // 回転処理
    }

    // 入力を取得
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // 横方向の入力を取得
        vAxis = Input.GetAxisRaw("Vertical"); // 縦方向の入力を取得
        wDown = Input.GetKey(KeyCode.LeftShift); // LeftShiftキーが押されているか確認
    }

    // 移動処理
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 入力から移動ベクトルを計算

        if (isDodge)
            moveVec = dodgeVec; // 回避中は回避方向に移動

        transform.position += moveVec * speed * (wDown ? 1.5f : 1.0f) * Time.deltaTime; // プレイヤーの位置を更新
    }

    // 回転処理
    void Turn()
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec); // 移動方向を向く
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 滑らかに回転
        }
    }

    // 回避処理
    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Space) && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec; // 回避方向を設定
            speed = originalSpeed * 2.5f; // 回避中の速度を設定
            isDodge = true; // 回避中状態を設定

            Invoke("EndDodge", dodgeTime); // 回避終了処理を一定時間後に呼び出す
        }
    }

    // 回避終了時の処理
    void EndDodge()
    {
        speed = originalSpeed; // 元の速度に戻す
        isDodge = false; // 回避状態を解除
    }
}
