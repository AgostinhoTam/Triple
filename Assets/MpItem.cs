using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpItem : MonoBehaviour
{
    public float recoveryAmount = 10f; // 回復量

    // トリガーでプレイヤーと衝突したときにMPを回復
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに"Mp"スクリプトがアタッチされているか確認
        Mp playerMp = other.GetComponent<Mp>();
        if (playerMp != null)
        {
            // MPを回復
            playerMp.ChangeMp(recoveryAmount);
            Debug.Log("MPが回復されました: " + recoveryAmount);

            // アイテムを消去
            Destroy(gameObject);
        }
    }
}
