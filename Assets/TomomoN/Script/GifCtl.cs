using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifCtl : MonoBehaviour
{
    public GameObject RawImage = null;
    public GameObject OnePicture = null;
    public GameObject ButtonObj = null;

    UniGifImage GifImage = null;
    HeartEffectGen HeartEffect = null;

    bool bGifPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        GifImage = RawImage.GetComponent<UniGifImage>();
        HeartEffect = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
    }

    // Update is called once per frame
    void Update()
    {
        // ハートの数が一定数超えない場合returnする
        HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
        if (heart.GetHeartCount() > heart.GetMaxHeartCount())
        {
            OnePicture.SetActive(false);
            ButtonObj.SetActive(false);
            bGifPlaying = true;
            GifImage.Play();
        }
        else
        {
            OnePicture.SetActive(true);
            ButtonObj.SetActive(true);

        }
    }


    public void SetGifPlaying(bool bPlaying) { bGifPlaying = bPlaying; }
    public bool IsGifPlaying() { return bGifPlaying; }
}
