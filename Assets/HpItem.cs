using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    // トリガーでプレイヤーと衝突したときにMPを回復
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに"Mp"スクリプトがアタッチされているか確認
        DamageSystem playerHp = other.GetComponent<DamageSystem>();
        if (playerHp != null)
        {
            // MPを回復
            playerHp.TakeDamage(-playerHp.GetMaxHealth()/2);
            Debug.Log("MPが回復されました: " + -playerHp.GetMaxHealth() / 2);

            // アイテムを消去
            Destroy(gameObject);
        }
    }
}
