using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTime : MonoBehaviour
{
    bool bSound = false;

    // Start is called before the first frame update
    void Start()
    {
        FadeManager_TomomoN.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウンが終わるまで処理に入らない
        if (Countdown.isStart && !bSound)
        {
            AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            audio.Play();
            bSound = true;
        }
    }
}
