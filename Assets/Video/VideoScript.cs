using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] VideoPlayer vp;

    Button button;

    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        
    }

    void TaskOnClick()
    {
        PauseVideo();
    }

    public void PauseVideo()
    {
        vp.Pause();
    }

    public void ResumeVideo()
    {
        vp.Play();
    }

    public void SkipVideo()
    {
        SceneManager.LoadScene("Text");
    }
}
