using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public Mp mpManager;
    DamageSystem m_DamageSystem;

    public HpUi hpui;

    // シーン切り替えに関する設定
    [Header("Scene Change Settings")]
    public float sceneChangeDelay = 1f; // シーン切り替えの前の待機時間
    public string gameOverSceneName = "GameOver"; // 切り替えるシーン名

    // 攻撃設定用クラス
    [System.Serializable]
    public class AttackSetting
    {
        public GameObject attackArea; // 攻撃範囲オブジェクト
        public KeyCode activationKey; // 攻撃を発動するキー
        public float attackAreaTime; // 攻撃範囲の持続時間
        public float gaugeConsumption; // ゲージの毎秒消費量
        public bool isActive = false; // 攻撃範囲がアクティブ中かどうか
    }

    public List<AttackSetting> attackSettings = new List<AttackSetting>(); // 攻撃設定のリスト

    // Start is called before the first frame update
    void Start()
    {
        m_DamageSystem = GetComponent<DamageSystem>();
        // 攻撃範囲を初期状態で非アクティブに設定
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

        // プレイヤーのHPが0以下の場合
        if (m_DamageSystem.GetHealth() <= 0)
        {
            hpui.ZeroHp(); // HP UIをゼロにする処理
            StartCoroutine(HandlePlayerDeath()); // プレイヤー死亡処理をコルーチンで実行
            return;
        }
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
                mpManager.ChangeMp(-attack.gaugeConsumption);
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

    // プレイヤーが死亡した際の処理
    private IEnumerator HandlePlayerDeath()
    {
        yield return new WaitForSeconds(sceneChangeDelay); // 指定した時間だけ待機

        // 指定されたシーンに移動
        SceneManager.LoadScene(gameOverSceneName);
    }
}
