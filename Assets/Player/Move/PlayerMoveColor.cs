using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveColor : MonoBehaviour
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

    private Renderer playerRenderer;

    public Color dodgeColor = Color.red; // 回避中の色
    private Color originalColor; // 元の色

    // 攻撃設定用クラス
    [System.Serializable]
    public class AttackSetting
    {
        public GameObject attackArea; // 攻撃範囲オブジェクト
        public KeyCode activationKey; // 攻撃を発動するキー
        public float attackAreaTime; // 攻撃範囲の持続時間
        public float gaugeConsumption; // ゲージの毎秒消費量
        public bool isActive = false; // 攻撃範囲がアクティブ中かどうか // 攻撃範囲がアクティブ中かどうか
    }

    public List<AttackSetting> attackSettings = new List<AttackSetting>(); // 攻撃設定のリスト

    void Start()
    {
        originalSpeed = speed; // ゲーム開始時に元の速度を保存
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color; // 元の色を保存

        // 攻撃範囲を初期状態で非アクティブに設定
        foreach (var attack in attackSettings)
        {
            if (attack.attackArea != null)
                attack.attackArea.SetActive(false);
        }
    }

    void Update()
    {
        GetInput(); // 入力を取得
        Move(); // 移動処理
        Dodge(); // 回避処理
        Turn(); // 回転処理
        HandleAttacks(); // 攻撃処理
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
            playerRenderer.material.color = dodgeColor; // 回避中の色に変更

            Invoke("EndDodge", dodgeTime); // 回避終了処理を一定時間後に呼び出す
        }
    }

    // 回避終了時の処理
    void EndDodge()
    {
        speed = originalSpeed; // 元の速度に戻す
        isDodge = false; // 回避状態を解除
        playerRenderer.material.color = originalColor; // 元の色に戻す
    }

    // 攻撃処理
    void HandleAttacks()
    {
        foreach (var attack in attackSettings)
        {
            // 攻撃範囲がアクティブ状態なら入力を無視
            if (attack.isActive)
                continue;

            // 指定キーが押された場合に攻撃処理を開始
            if (Input.GetKeyDown(attack.activationKey) && attack.attackArea != null)
            {
                StartCoroutine(ActivateAttackArea(attack));
            }
        }
    }

    // 攻撃範囲を一定時間アクティブ化
    private IEnumerator ActivateAttackArea(AttackSetting attack)
    {
        attack.isActive = true; // 攻撃範囲がアクティブ中状態に設定
        attack.attackArea.SetActive(true); // 攻撃範囲をアクティブ化

        yield return new WaitForSeconds(attack.attackAreaTime); // 持続時間待機

        attack.attackArea.SetActive(false); // 攻撃範囲を非アクティブ化
        attack.isActive = false; // 攻撃範囲のアクティブ状態を解除
    }
}
