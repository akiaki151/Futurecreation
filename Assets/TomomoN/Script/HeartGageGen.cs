using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGageGen : MonoBehaviour
{
    GameObject HeartGage = null;

    // Start is called before the first frame update
    void Start()
    {
        HeartGage = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GifCtl gif = GameObject.Find("GifControl").GetComponent<GifCtl>();
        if (gif.IsGifPlaying())
        {
            HeartGage.SetActive(false);
        }
        else
        {
            HeartGage.SetActive(true);
        }

        // ハートの数が一定数超えない場合returnする
        //HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
        //if (heart.GetHeartCount() > heart.GetMaxHeartCount())
        //{
        //    HandTexture.SetActive(false);
        //}
        //else
        //{
        //    HandTexture.SetActive(true);
        //}
    }
}
