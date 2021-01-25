using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public SceneController Sc;
    private GameObject canvas;
    void Start()
    {
        Sc = new SceneController(this);
        
        if (Actions.g_index != 0)
        {
            canvas = GameObject.Find("Canvas");
            foreach (Transform child in canvas.transform)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (child.name == "TitleWindow" || child.name == "ExitTitleConfirmationPanel" || child.name == "SettingWindow" || child.name == "SaveLoadWindow" ||child.name== "Fade")
                    {
                        child.gameObject.SetActive(false);
                    }
                    else
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
            Sc.SetScene("010");
        }
        else
        {
            SetFirstScene();
        }
    }

    void Update()
    {
        Sc.WaitClick();
        Sc.SetComponents();
    }

    void SetFirstScene()
    {
        Sc.SetScene("001");
    }

    public void SetSelect1Scene()
    {
        Sc.SetScene("002");
    }

    public void SetNadeScene1()
    {
        SceneManager.LoadScene("Heroine001");
    }

    public void SetSelect2Scene()
    {
        Sc.SetScene("003");
    }

    public void SetSelect3Scene()
    {
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "TextWindow")
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.name == "TextPanel")
                    {
                        foreach (Transform child3 in child2.transform)
                        {
                            if (child3.name == "Text")
                            {
                                child3.gameObject.GetComponent<Text>().text="";
                            }
                        }
                    }
                }
            }
        }
        Sc.SetScene("111");
    }

    public void SetNadeScene2()
    {
        SceneManager.LoadScene("Heroine002");
    }
}
