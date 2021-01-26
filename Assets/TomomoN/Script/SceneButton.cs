using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    Fade_Anime _Fade;
    GameObject Hand = null;
    public void OnClickSceneButton()
    {
        GifTextureScript gif = GameObject.Find("GifImage").GetComponent<GifTextureScript>();
        if (!gif.IsGifPlaying())
        {
            Hand = GameObject.Find("HandPrefab");
            Hand.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _Fade = GameObject.Find("FadeImage").GetComponent<Fade_Anime>();
            StartCoroutine(_Fade.FadeIn());

        }
    }
}
