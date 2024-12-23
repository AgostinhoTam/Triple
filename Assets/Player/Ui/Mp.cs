using UnityEngine;
using UnityEngine.UI;

public class Mp : MonoBehaviour
{
    public float maxMp = 100f; // 最大MP
    private float curMp; // 現在のMP
    private float displayedMp; // 表示用のMP（スムーズに更新される値）

    public Slider mpBarSlider; // MPバーのスライダー
    public float smoothSpeed = 5f; // スムーズな変化速度

    void Start()
    {
        curMp = maxMp; // MPを最大値で初期化
        displayedMp = maxMp; // 表示用MPも最大値で初期化
        UpdateMpBar(); // MPバーを初期化
    }

    void Update()
    {
        // 表示用のMPをスムーズに現在のMPに近づける
        displayedMp = Mathf.Lerp(displayedMp, curMp, Time.deltaTime * smoothSpeed);
        UpdateMpBar(); // MPバーを更新
    }

    // MPを変更するメソッド
    public void ChangeMp(float amount)
    {
        curMp = Mathf.Clamp(curMp + amount, 0, maxMp); // MPの範囲を制限
        Debug.Log(curMp);
        if (curMp == maxMp)
        {
            mpBarSlider.value = 1.0f;
        }
        UpdateMpBar(); // MPバーを更新
    }

    // 現在のMPを取得するメソッド
    public float GetCurrentMp()
    {
        return curMp;
    }

    // MPバーのスライダーを更新
    private void UpdateMpBar()
    {
        if (mpBarSlider != null)
        {
            mpBarSlider.value = displayedMp / maxMp; // スライダーの値を更新
        }
    }
}
