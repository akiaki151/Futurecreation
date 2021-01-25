using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Actions
{
    GameController gc;
    private GameObject canvas;
    public static int g_index = 0;


    public Actions(GameController gc)
    {
        this.gc = gc;
    }

    //行いたい処理などを書いていく
    public void SelectAction(int index)
    {
        switch (index)
        {
            case 0:
                gc.Sc.SetScene("004");
                break;
            case 1:
                gc.Sc.ActionMove();
                break;
            case 2:
                Title();
                break;
            case 3:
                SceneManager.LoadScene("Heroine001");
                break;
            case 4:
                Title_Start();
                break;
            case 5:
                SceneManager.LoadScene("Heroine002");
                break;
            case 6:
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
                                        child3.gameObject.GetComponent<Text>().text = "";
                                    }
                                }
                            }
                        }
                    }
                }
                break;
            case 7:
                CompIndex(1);
                break;
            case 8:
                CompIndex(2);
                break;
            default:
                break;
        }
    }

    public void CompIndex(int index)
    {
        g_index = index;
    }

    public void Updata()
    {

    }

    private void Title()
    {
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            for (int i = 0; i < 20; i++)
            {
                if (child.name != "TitleWindow")
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Title_Start()
    {
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            for (int i = 0; i < 20; i++)
            {
                if (child.name == "TitleWindow" || child.name == "ExitTitleConfirmationPanel" || child.name == "SettingWindow"||child.name== "SaveLoadWindow"|| child.name == "NotSaveData")
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
