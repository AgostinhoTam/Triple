using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveColor : MonoBehaviour
{
    public float speed; // 移動速度
    public float dodgeTime;//回転持続時間

    private float originalSpeed; // 元の移動速度を保存

    float hAxis; // 横方向の入力
    float vAxis; // 縦方向の入力

    Vector3 moveVec; // 移動ベクトル
    Vector3 dodgeVec; // 回避ベクトル

    bool wDown; // 走る状態かどうか
    bool isDodge; // 回避中かどうか

    private Renderer playerRenderer;

    public Color dodgeColor = Color.red;
    private Color originalColor;
    void Start()
    {
        originalSpeed = speed; // ゲーム開始時に元の速度を保存
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color; 
    }

    void Update()
    {
        GetInput(); // 入力を取得
        Move(); // 移動処理
        Dodge(); // 回避処理
        Turn(); // 回転
    }

    //入力を取得
    void GetInput()
    {
        // WASD入力を取得
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        // LeftShiftキーで走る状態を確認
        wDown = Input.GetKey(KeyCode.LeftShift);
    }

    //移動
    void Move()
    {
        // 移動ベクトルを計算し、正規化して方向を取得
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        // プレイヤーの位置を更新:
        // 移動ベクトル (moveVec) に移動速度 (speed) を掛ける
        transform.position += moveVec * speed * (wDown ? 1.5f : 1.0f) * Time.deltaTime;
    }

    //移動した時の回転
    void Turn()
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 회전 속도 조절
        }
    }


    //回避
    void Dodge()
    {
        // スペースキーを押したとき回避処理を実行
        if (Input.GetKeyDown(KeyCode.Space) && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec;
            speed = originalSpeed * 2.5f; // 元の速度を基に回避速度を計算
            isDodge = true;
            playerRenderer.material.color = dodgeColor;

            Invoke("EndDodge", dodgeTime); 
        }
    }
    //回避がおわった時の処理
    void EndDodge()
    {
        speed = originalSpeed; // 回避終了時に元の速度に戻す
        isDodge = false; // 回避状態を解除
        playerRenderer.material.color = originalColor;
    }


}
