using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    public DamageSystem hp; // HPを管理するスクリプト
    private float displayedHp; // 表示用のHP（スムーズに更新される値）

    public Slider hpBarSlider; // HPバーのスライダー
    public float smoothSpeed = 8f; // スムーズな変化速度

    void Start()
    {
        displayedHp = hp.GetMaxHealth(); // 表示用HPを現在のHPで初期化
        UpdateHpBar(); // HPバーを初期化
    }

    void Update()
    {
        // 表示用のHPをスムーズに現在のHPに近づける
        displayedHp = Mathf.MoveTowards(displayedHp, hp.GetHealth(), smoothSpeed * Time.deltaTime);
        UpdateHpBar(); // HPバーを更新
    }

    // HPバーのスライダーを更新
    private void UpdateHpBar()
    {
        if (hpBarSlider != null)
        {
            hpBarSlider.value = displayedHp / hp.GetMaxHealth(); // スライダーの値を更新
        }
    }
    public void ZeroHp()
    {
        hpBarSlider.value = 0.0f;
    }
}
