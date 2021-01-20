using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifCtl : MonoBehaviour
{
    public GameObject RawImage = null;
    public GameObject OnePicture = null;

    UniGifImage GifImage = null;
    HeartEffectGen HeartEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        GifImage = RawImage.GetComponent<UniGifImage>();
        HeartEffect = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HeartEffect.GetHeartCount() == 20)
        {
            OnePicture.SetActive(false);
            GifImage.Play();
            HeartEffect.SetHeartCount(0);
        }
    }
}
