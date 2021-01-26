using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifTextureScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = null;
    [SerializeField] private float changeFrameSecond;
    [SerializeField] private float LoopCount;

    Image GifImage = null;
    float dTime = 0.0f;
    int frameNum = 0;
    int loopNum = 0;
    bool bGifPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        GifImage = this.GetComponent<Image>();
        dTime = 0.0f;
        frameNum = 0;
        loopNum = 0;
        bGifPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bGifPlaying) return;

        dTime += Time.deltaTime;
        if (changeFrameSecond < dTime)
        {
            dTime = 0.0f;
            frameNum++;

            if (frameNum >= sprites.Count)
            {
                frameNum = 0;

                loopNum++;
                if (loopNum >= LoopCount)
                {
                    HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
                    heart.SetHeartCount(0);
                    bGifPlaying = false;
                }
            }
        }
        GifImage.sprite = sprites[frameNum];
    }


    public void SetGifPlaying(bool bPlaying) { bGifPlaying = bPlaying; }
    public bool IsGifPlaying() { return bGifPlaying; }

    public void Play()
    {
        if (bGifPlaying) return;

        dTime = 0.0f;
        frameNum = 0;
        loopNum = 0;
        bGifPlaying = true;
    }

    public void Stop()
    {
        if (!bGifPlaying) return;

        bGifPlaying = false;
    }
}
