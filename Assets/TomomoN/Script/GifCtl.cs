using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifCtl : MonoBehaviour
{
    public GameObject GifObj = null;
    public GameObject OnePicture = null;
    public GameObject ButtonObj = null;

    GifTextureScript GifImage = null;
    HeartEffectGen HeartEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        GifImage = GifObj.GetComponent<GifTextureScript>();
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
            GifImage.Play();
        }
        else
        {
            OnePicture.SetActive(true);
            ButtonObj.SetActive(true);

        }
    }
}
