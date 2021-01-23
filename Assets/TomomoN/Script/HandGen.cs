using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGen : MonoBehaviour
{
    GameObject HandTexture = null;

    // Start is called before the first frame update
    void Start()
    {
        HandTexture = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // 一旦処理を止めます
        //if (true) return;

        // ハートの数が一定数超えない場合returnする
        HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
        if (heart.GetHeartCount() > heart.GetMaxHeartCount())
        {
            HandTexture.SetActive(false);
        }
        else
        {
            HandTexture.SetActive(true);
        }
        // 終了ボタンの上に乗ってるとき
        //if (HandTexture.GetComponent<HandCtl>().IsOnButtont())
        //{
        //    Debug.Log("ボタン消す");
        //    HandTexture.SetActive(false);
        //}
        //else
        //{
        //    Debug.Log("ボタン復活");
        //    HandTexture.SetActive(true);
        //}
    }



    //void OnMouseOver()
    //{
    //    Debug.Log("OnMouseOver");
    //    HandTexture.SetActive(false);
    //}
    //void OnMouseExit()
    //{
    //    Debug.Log("OnMouseExit");
    //    HandTexture.SetActive(true);
    //}
}
