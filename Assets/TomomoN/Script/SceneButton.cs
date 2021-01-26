using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public void OnClickSceneButton()
    {
        GifTextureScript gif = GameObject.Find("GifImage").GetComponent<GifTextureScript>();
        if (!gif.IsGifPlaying())
        {
            FadeManager_TomomoN.FadeOut("Text");
        }
    }
}
