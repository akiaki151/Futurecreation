using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEffectGen : MonoBehaviour
{
    public GameObject GeneratePoint = null;
    public GameObject HeartEffect = null;

    const int maxHeartCount = 300;

    float heartAddSpan = 0.02f;
    float heartSubSpan = 1.5f;
    float heartDelta = 0.0f;
    float SE_Span = 0.1f;
    float SE_delta = 0.0f;
    int heartCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 手が動いているか？
        HandCtl hand = GeneratePoint.transform.GetChild(0).GetComponent<HandCtl>();
        if (!hand.IsMovement())
        {
            // Gif再生中か？
            GifCtl gif = GameObject.Find("GifControl").GetComponent<GifCtl>();
            if (gif.IsGifPlaying()) return;

            heartDelta += Time.deltaTime;    // デルタを加算
            if (heartDelta > heartSubSpan)
            {
                heartDelta = 0.0f;   // デルタをリセット

                // ハートの数を減算
                heartCount--;
                if (heartCount < 0)
                {
                    heartCount = 0;
                }
            }
        }
        else
        {
            // なでなで中か？
            if (!HandCtl.isNadeNade) return;

            // Gif再生中か？
            GifCtl gif = GameObject.Find("GifControl").GetComponent<GifCtl>();
            if (gif.IsGifPlaying()) return;

            SE_delta += Time.deltaTime;    // デルタを加算
            if (SE_delta > SE_Span)
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.Play();

                SE_delta = 0.0f;   // デルタをリセット
            }
            heartDelta += Time.deltaTime;    // デルタを加算
            if (heartDelta > heartAddSpan)
            {
                heartDelta = 0.0f;   // デルタをリセット

                // ハートの数を加算
                heartCount++;
            }
        }

        
        // ハートの数が一定数超えたか？
        HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
        if (heartCount == heart.GetMaxHeartCount())
        {
            // 生成
            GameObject effect = Instantiate(HeartEffect) as GameObject;
            effect.transform.position = new Vector3(0.0f, -4.5f, 0.06f);
            effect.transform.Rotate(0.0f, 0.0f, 0.0f);

            // 子オブジェクトにセットする
            effect.transform.parent = this.transform;

            heartCount++;

            return;
        }


        //delta += Time.deltaTime;    // デルタを加算
        //if (delta > span)
        //{
        //    AudioSource audio = this.GetComponent<AudioSource>();
        //    audio.Play();

        //    delta = 0.0f;   // デルタをリセット

        //    // 生成
        //    GameObject effect = Instantiate(HeartEffect) as GameObject;
        //    effect.transform.position = new Vector3(GeneratePoint.transform.GetChild(0).position.x, GeneratePoint.transform.GetChild(0).position.y, 0.06f);
        //    effect.transform.Rotate(0.0f, 0.0f, 0.0f);

        //    // 子オブジェクトにセットする
        //    effect.transform.parent = this.transform;

        //    // ハートの数を加算
        //    heartCount++;
        //}

    }


    public int GetHeartCount() { return heartCount; }
    public int GetMaxHeartCount() { return maxHeartCount; }
    public void SetHeartCount(int HeartCount) { heartCount = HeartCount; }
}
