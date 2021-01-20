using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager_TomomoN : MonoBehaviour
{

    // フェード用のCanvasとImage
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    // フェード用Imageの透明度
    private static float alpha = 0.0f;

    // フェードインアウトのフラグ
    private static bool bFadeIn = false;

    // フェードしたい時間（単位は秒）
    private static float fadeTime = 1.5f;



    // フェード用のCanvasとImage生成
    static void Init()
    {

        // フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager_TomomoN>();

        // 最前面になるよう適当なソートオーダー設定
        fadeCanvas.sortingOrder = 100;

        // フェード用のImage生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        // Imageサイズは適当に大きく設定してください
        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);

    }

    // フェードイン開始
    public static void FadeIn()
    {

        if (fadeImage == null) Init();
        alpha = 1.0f;
        fadeImage.color = Color.black;
        bFadeIn = true;

    }

    void Update()
    {

        // カウントダウンが終わるまで処理に入らない
        if (!Countdown.isStart) return;

        // フラグ有効なら毎フレームフェードイン/アウト処理
        if (bFadeIn)
        {
            // 経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime;

            // フェードイン終了判定
            if (alpha <= 0.0f)
            {
                Countdown.isStart = false;
                bFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }

            // フェード用Imageの色・透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }

    }



    // ゲッター
    public static bool IsFadeIn() { return bFadeIn; }

}