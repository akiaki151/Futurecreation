using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    Fade_Anime _Fade;
    public void OnClickSceneButton()
    {
        GifTextureScript gif = GameObject.Find("GifImage").GetComponent<GifTextureScript>();
        if (!gif.IsGifPlaying())
        {
            _Fade = GameObject.Find("FadeImage").GetComponent<Fade_Anime>();
            StartCoroutine(_Fade.FadeIn());
        }
    }
}
