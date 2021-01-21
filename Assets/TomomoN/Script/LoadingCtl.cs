using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCtl : MonoBehaviour
{
    //　シーンロード中に表示するUI画面
    [SerializeField] private GameObject loadUI;

    //　読み込み率を表示するスライダー
    [SerializeField] private Slider slider;

    //　読み込み率を表示するスライダー
    [SerializeField] private float LoadingTime = 3.0f;

    private float seconds = 0.0f; // 制限時間（秒）
    private float totalTime;    // トータル制限時間
    private float oldSeconds;   // 前回Update時の秒数

    void Start()
    {

        loadUI.SetActive(true);

        totalTime = seconds + 1.0f;
        oldSeconds = totalTime;

    }

    void Update()
    {
        // 制限時間が0秒以下なら何もしない
        if (totalTime >= LoadingTime) return;


        // 一旦トータルの制限時間を計測
        totalTime = seconds;
        totalTime += Time.deltaTime;

        // 再設定
        seconds = totalTime;

        slider.value = seconds / LoadingTime;

        // 過去秒数を保存
        oldSeconds = seconds;

        // 制限時間以下になったらコンソールに「制限時間終了」という文字列を表示する
        if (totalTime >= LoadingTime)
        {
            Debug.Log("ロード終了");
            loadUI.SetActive(false);
        }
    }
}
