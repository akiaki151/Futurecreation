using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    Fade_Anime _Fade;
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "FadeImage")
            {
                _Fade = child.gameObject.GetComponent<Fade_Anime>();
            }
        }
        _Fade.gameObject.SetActive(true);
        StartCoroutine(_Fade.FadeOut());
    }

}
