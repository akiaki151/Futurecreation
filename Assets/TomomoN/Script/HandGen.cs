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
        GifCtl gif = GameObject.Find("GifControl").GetComponent<GifCtl>();
        if (gif.IsGifPlaying())
        {
            HandTexture.SetActive(false);
        }
        else
        {
            HandTexture.SetActive(true);
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
