using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
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
