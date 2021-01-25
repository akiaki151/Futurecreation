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
        vp.isLooping = true;
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        vp.loopPointReached += SkipVideo2;
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

    public void SkipVideo2(VideoPlayer vp)
    {
        vp.Stop();
        SceneManager.LoadScene("Text");
    }

    public void SkipVideo()
    {
        SceneManager.LoadScene("Text");
    }
}
