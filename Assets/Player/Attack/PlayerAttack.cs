using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public float maxMp; // MPの最大値
    protected float curMp; // 現在のMP
    public Slider mpBarSlider; // MPバーのスライダー
    public float smoothSpeed = 5f; // ゲージが変化する速度

    // 攻撃設定用クラス
    [System.Serializable]
    public class AttackSetting
    {
        public GameObject attackArea; // 攻撃範囲のオブジェクト
        public KeyCode activationKey; // 攻撃を発動するキー
        public float attackAreaTime; // 攻撃範囲の持続時間
        public float mpConsumption; // ゲージの消費量
        public bool isActive = false; // 攻撃範囲がアクティブ中かどうか
    }

    public List<AttackSetting> attackSettings = new List<AttackSetting>(); // 攻撃設定のリスト

    void Start()
    {
        // 攻撃範囲を初期状態で非アクティブに設定
        foreach (var attack in attackSettings)
        {
            if (attack.attackArea != null)
                attack.attackArea.SetActive(false);
        }

        curMp = maxMp; // 現在のMPを最大値に設定
    }

    void Update()
    {
        HandleAttacks(); // 攻撃処理
        CheckMp(); // MPバーの更新
    }

    // 攻撃処理
    void HandleAttacks()
    {
        foreach (var attack in attackSettings)
        {
            // 攻撃範囲がアクティブ中の場合は入力を無視
            if (attack.isActive)
                continue;

            // 指定キーが押された場合に攻撃処理を開始
            if (Input.GetKeyDown(attack.activationKey) && attack.attackArea != null)
            {
                StartCoroutine(ActivateAttackArea(attack)); // 攻撃範囲をアクティブ化
                curMp -= attack.mpConsumption; // MPを消費
            }
        }
    }

    // 攻撃範囲を一定時間アクティブ化
    private IEnumerator ActivateAttackArea(AttackSetting attack)
    {
        attack.isActive = true; // 攻撃範囲をアクティブ中に設定
        attack.attackArea.SetActive(true); // 攻撃範囲をアクティブ化

        yield return new WaitForSeconds(attack.attackAreaTime); // 指定時間待機

        attack.attackArea.SetActive(false); // 攻撃範囲を非アクティブ化
        attack.isActive = false; // アクティブ状態を解除
    }

    // MPバーの更新
    public void CheckMp()
    {
        if (mpBarSlider != null)
        {
            // MPバーをスムーズに更新
            float targetValue = curMp / maxMp; // 目標値
            mpBarSlider.value = Mathf.Lerp(mpBarSlider.value, targetValue, Time.deltaTime * smoothSpeed); // スムーズに値を変化
        }
    }
}