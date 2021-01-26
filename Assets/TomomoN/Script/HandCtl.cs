using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCtl : MonoBehaviour
{
    //public Sprite ChangeImage001;

    //Image Imageinfo;
    public static bool isNadeNade = false;

    const float maxDeg = 1.5f;
    const float minDeg = -1.5f;

    string NadeNadeRangeName;
    Vector3 OldPosition;
    float t = 0.0f;

    //bool bOnButton = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(-8.2f, -3.2f, 0.0f);
        //Imageinfo = this.GetComponent<Image>();
    }

    void OnEnable()
    {
        this.transform.position = new Vector3(-8.2f, -3.2f, 0.0f);
        //Imageinfo = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeManager_TomomoN.IsFadeIn()) return;
        if (FadeManager_TomomoN.IsFadeOut()) return;

        // 1フレーム前の座標を保存
        OldPosition = this.transform.position;

        //if (Input.GetMouseButton(0))    // 仮（条件によって変更）
        //{
        //}
        if (!isNadeNade)
        {
            // マウス座標取得
            Vector3 handPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Z座標を0にする
            handPosition = new Vector3(handPosition.x, handPosition.y, 0.0f);
            this.transform.position = handPosition;
        }
        else
        {
            /* なでなで中の処理 */

            GifTextureScript gif = GameObject.Find("GifImage").GetComponent<GifTextureScript>();
            if (gif.IsGifPlaying()) return;

            // マウス座標取得
            Vector3 handPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            NadeNadeCtl nadenade = GameObject.Find("OnePicturePrefab").GetComponent<NadeNadeCtl>();
            Vector3 startPos, endPos, ctlPos;


            // どのなでなで位置か？
            if (NadeNadeRangeName == "NadeNadeRange")
            {
                startPos = nadenade.GetStartPoint(0);
                endPos = nadenade.GetEndPoint(0);
                ctlPos = nadenade.GetCtlPoint(0);
            }
            else if (NadeNadeRangeName == "NadeNadeRange (1)")
            {
                startPos = nadenade.GetStartPoint(1);
                endPos = nadenade.GetEndPoint(1);
                ctlPos = nadenade.GetCtlPoint(1);
            }
            else
            {
                startPos = nadenade.GetStartPoint(2);
                endPos = nadenade.GetEndPoint(2);
                ctlPos = nadenade.GetCtlPoint(2);
            }



            // 傾きの大きさによって撫でる感覚を変える
            float deg = (endPos.y - startPos.y) / (endPos.x - startPos.x);
            if (minDeg < deg && deg < maxDeg)
            {
                // 横長
                t = (handPosition.x - startPos.x) / (endPos.x - startPos.x);
            }
            else
            {
                // 縦長
                t = (handPosition.y - startPos.y) / (endPos.y - startPos.y);
            }
            t = Mathf.Clamp01(t);   // 値を0～1に収める


            /* ベジェ曲線 */
            Vector3 p0 = Vector3.Lerp(startPos, ctlPos, t);  // 始点と制御点で内分
            Vector3 p1 = Vector3.Lerp(ctlPos, endPos, t);    // 制御点と終点で内分

            handPosition = Vector3.Lerp(p0, p1, t);     // p0とp1で内分で曲線が求まる



            //Z座標を0にする
            handPosition = new Vector3(handPosition.x, handPosition.y, 0.0f);
            this.transform.position = handPosition;


            // マウス座標取得
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (minDeg < deg && deg < maxDeg)
            {
                // 横長
                if (mousePosition.x < startPos.x - 1.0f || endPos.x + 1.0f < mousePosition.x)
                {
                    HandCtl.isNadeNade = false;
                }
                if (startPos.y < endPos.y)
                {
                    if (mousePosition.y < startPos.y - 1.0f || endPos.y + 2.0f < mousePosition.y)
                    {
                        HandCtl.isNadeNade = false;
                    }
                }
                if (startPos.y > endPos.y)
                {
                    if (mousePosition.y > startPos.y + 2.0f || endPos.y - 1.0f > mousePosition.y)
                    {
                        HandCtl.isNadeNade = false;
                    }
                }
            }
            else
            {
                // 縦長
                if (mousePosition.y > startPos.y || endPos.y > mousePosition.y)
                {
                    HandCtl.isNadeNade = false;
                }
                if (startPos.x < endPos.x)
                {
                    if (mousePosition.x < startPos.x || endPos.x < mousePosition.x)
                    {
                        HandCtl.isNadeNade = false;
                    }
                }
                if (startPos.x > endPos.x)
                {
                    if (mousePosition.x > startPos.x || endPos.x > mousePosition.x)
                    {
                        HandCtl.isNadeNade = false;
                    }
                }
            }

        }
    }


    public bool IsMovement() { return (OldPosition != this.transform.position); }
    //public bool IsOnButtont() { return bOnButton; }


    void OnTriggerStay2D(Collider2D t)
    {
        if (t.gameObject.name != "Button")
        {
            HandCtl.isNadeNade = true;
            NadeNadeRangeName = t.gameObject.name;
        }
        //else
        //{
        //    Debug.Log("ボタン上");
        //    bOnButton = true;
        //}
    }
    void OnTriggerExit2D(Collider2D t)
    {
        //if (t.gameObject.name == "Button")
        //{
        //    Debug.Log("ボタン外");
        //    bOnButton = false;
        //}
        ////else
        //{
        //    HandCtl.isNadeNade = false;
        //    Debug.Log("はずれた");
        //}
    }
}