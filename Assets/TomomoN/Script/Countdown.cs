using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private float seconds = 3.0f; // 制限時間（秒）

    private float totalTime;    // トータル制限時間
    private float oldSeconds;   // 前回Update時の秒数

    public static bool isStart = false;

    void Start()
    {

        totalTime = seconds + 1.0f;

        oldSeconds = totalTime;
        seconds += 1.0f;

    }

    void Update()
    {
        // 制限時間が0秒以下なら何もしない
        if (totalTime <= 0.0f) return;


        // 一旦トータルの制限時間を計測
        totalTime = seconds;
        totalTime -= Time.deltaTime;

        // 再設定
        seconds = totalTime;
        
        // 過去秒数を保存
        oldSeconds = seconds;

        // 制限時間以下になったらコンソールに「制限時間終了」という文字列を表示する
        if (totalTime <= 0.0f)
        {
            isStart = true;
        }
    }
}
