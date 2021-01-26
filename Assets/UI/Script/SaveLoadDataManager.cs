using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadDataManager : MonoBehaviour
{
    [SerializeField] private GameObject SaveLoadWindow;
    [SerializeField] private GameObject PageTitle;
    [SerializeField] private GameObject ConfirmationPanel;

    [SerializeField] private Button SaveButton, LoadButton, QuickLoadButton, ChangeCommentButton, ReturnButton;

    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    private GameObject TitleWindow;

    private enum SaveLoadIndex
    {
        NONE = 0,
        SAVE,
        LOAD,
        QUICKLOAD,
        CHANGECOMMENT,
        RETURN
    }

    public int CurrentSaveLoadIndex;
    private int NextSaveLoadIndex;

    private void Awake()
    {
        TitleWindow = GameObject.Find("TitleWindow");
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentSaveLoadIndex = 0;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        SaveButton.colors = newColor;
        LoadButton.colors = newColor;
        ChangeCommentButton.colors = newColor;
        ReturnButton.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        SaveButton.colors = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentSaveLoadIndex == NextSaveLoadIndex) return;
        CurrentSaveLoadIndex = NextSaveLoadIndex;

        if (TitleWindow.activeInHierarchy)
        {
            CurrentSaveLoadIndex = 2;
        }
        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        SaveButton.colors = newColor;
        LoadButton.colors = newColor;
        ChangeCommentButton.colors = newColor;
        ReturnButton.colors = newColor;

        switch (CurrentSaveLoadIndex)
        {
            case 1:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                SaveButton.colors = newColor;
                ConfirmationPanel.GetComponent<SaveLoadConfirmationPanelManager>().SetIndex(1);

                //PageTitle.GetComponent<Image>().sprite = PageTitleSave;

                break;
            case 2:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                LoadButton.colors = newColor;
                ConfirmationPanel.GetComponent<SaveLoadConfirmationPanelManager>().SetIndex(2);

                //PageTitle.GetComponent<Image>().sprite = PageTitleSave;

                break;
            case 3:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                QuickLoadButton.colors = newColor;

                //PageTitle.GetComponent<Image>().sprite = PageTitleSave;

                break;
            case 4:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                ChangeCommentButton.colors = newColor;

                break;
        }
    }

    public void SetSaveLoadIndex(int index)
    {
        NextSaveLoadIndex = index;
    }

    public int GetSaveLoadIndex()
    {
        return CurrentSaveLoadIndex;
    }
}
