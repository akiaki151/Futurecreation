using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    [SerializeField] VideoPlayer vp;
    [SerializeField] GameObject ConfirmationPanel;

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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ConfirmationPanel.GetComponent<TwoOptionButton>().SetIndex(1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ConfirmationPanel.GetComponent<TwoOptionButton>().SetIndex(2);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (ConfirmationPanel.GetComponent<TwoOptionButton>().GetIndex())
            {
                case 1:
                    ConfirmationPanel.GetComponent<TwoOptionButton>().SetIndex(1);
                    ConfirmationPanel.gameObject.SetActive(false);
                    SkipVideo();
                    break;
                case 2:
                    ConfirmationPanel.GetComponent<TwoOptionButton>().SetIndex(2);
                    ConfirmationPanel.gameObject.SetActive(false);
                    ResumeVideo();
                    break;
            }
        }
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
        vp.Stop();
        SceneManager.LoadScene("Text");
    }
}
