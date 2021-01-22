using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] VideoPlayer vp;

    void Start()
    {
        
    }

    void Update()
    {
        if (vp.isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Text");
            }
        }
        else
        {
            SceneManager.LoadScene("Text");
        }
    }

}
