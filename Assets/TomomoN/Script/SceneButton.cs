using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public void OnClickSceneButton()
    {
        GifCtl gif = GameObject.Find("GifControl").GetComponent<GifCtl>();
        if (!gif.IsGifPlaying())
        {
            Debug.Log("シーン遷移");
            SceneManager.LoadScene("Text");
        }
    }
}
