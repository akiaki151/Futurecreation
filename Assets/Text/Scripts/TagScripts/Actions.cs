using UnityEngine;
using UnityEngine.SceneManagement;

public class Actions
{
    GameController gc;
    private GameObject canvas;
    private GameObject targetGameObject;

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
            default:
                break;
        }
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
                    targetGameObject = child.gameObject;
                    targetGameObject.SetActive(false);
                }
            }
        }
    }
}
