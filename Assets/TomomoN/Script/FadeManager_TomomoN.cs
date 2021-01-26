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
    private static bool bFadeOut = false;

    // フェードしたい時間（単位は秒）
    private static float fadeTime = 1.5f;

    // 遷移先のシーンName
    private static string nextScene = "";

    private static float t = 0.0f;



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
        t = 0.0f;
        alpha = 1.0f;
        fadeImage.color = Color.black;
        bFadeIn = true;

    }

    // フェードアウト開始
    public static void FadeOut(string NextScene)
    {

        if (fadeImage == null) Init();
        nextScene = NextScene;
        alpha = 0.0f;
        fadeImage.color = Color.clear;
        fadeCanvas.enabled = true;
        bFadeOut = true;

    }

    void Update()
    {
        // フラグ有効なら毎フレームフェードイン/アウト処理
        if (bFadeIn)
        {
            // カウントダウンが終わるまで処理に入らない
            //if (!Countdown.isStart) return;

            t += Time.deltaTime;
            if (Timer.LoadTime > t) return;

            // 経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime;

            // フェードイン終了判定
            if (alpha <= 0.0f)
            {
                //Countdown.isStart = false;
                bFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }

            // フェード用Imageの色・透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (bFadeOut)
        {
            // 経過時間から透明度計算
            alpha += Time.deltaTime / fadeTime;
            
            // フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                bFadeOut = false;
                alpha = 1.0f;

                // 次のシーンへ遷移
                SceneManager.LoadScene(nextScene);
            }

            // フェード用Imageの色・透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }

    }



    // ゲッター
    public static bool IsFadeIn() { return bFadeIn; }
    public static bool IsFadeOut() { return bFadeOut; }

}