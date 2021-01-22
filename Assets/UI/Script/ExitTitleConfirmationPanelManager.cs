using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitTitleConfirmationPanelManager : MonoBehaviour
{
    [SerializeField] Sprite TitleImage, ExitImage;

    [SerializeField] private GameObject panel;
    private GameObject canvas;
    private GameObject targetGameObject;
    private Button Button_1;

    private enum ConfirmationIndex
    {
        NONE = 0,
        TITLE,
        EXIT,
    }

    [SerializeField] private int CurrentIndex;
    [SerializeField] private int NextIndex;

    private GameObject Gameobject;
    private Save save;

    // Start is called before the first frame update
    void Start()
    {
        CurrentIndex = 0;
        Button_1 = this.GetComponentsInChildren<Button>()[0];
        Button_1.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (NextIndex == CurrentIndex) return;

        CurrentIndex = NextIndex;

        switch (CurrentIndex)
        {
            case 1:
                transform.Find("Image").GetComponent<Image>().sprite = TitleImage;
                break;
            case 2:
                transform.Find("Image").GetComponent<Image>().sprite = ExitImage;
                break;
        }
    }

    void TaskOnClick()
    {
        switch (CurrentIndex)
        {
            case 1:
                panel.GetComponent<TwoOptionButton>().SetIndex(1);
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
                panel.gameObject.SetActive(false);
                break;
            case 2:
                panel.GetComponent<TwoOptionButton>().SetIndex(1);
                #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
                panel.gameObject.SetActive(false);
                break;
        }
    }

    public void SetIndex(int index)
    {
        NextIndex = index;
    }

    public int GetIndex()
    {
        return CurrentIndex;
    }
}
