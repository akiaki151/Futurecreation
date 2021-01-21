using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingWindow;
    [SerializeField] private GameObject GeneralSettingPanel, TextSettingPanel, SoundSettingPanel, ShortcutSettingPanel, DialougeSettingPanel;

    [SerializeField] private Button GeneralSettingButton, TextSettingButton, SoundSettingButton, ShortcutSettingButton, DialougeSettingButton;

    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    private enum SettingIndex
    {
        NONE = 0,
        GENERAL,
        TEXT,
        SOUND,
        SHORTCUT,
        DIALOUGE
    }

    private int CurrentSettingIndex;
    private int NextSettingIndex;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSettingIndex = NextSettingIndex = 0;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        GeneralSettingButton.colors = newColor;
        TextSettingButton.colors = newColor;
        SoundSettingButton.colors = newColor;
        ShortcutSettingButton.colors = newColor;
        DialougeSettingButton.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        GeneralSettingButton.colors = newColor;

        GeneralSettingPanel.SetActive(true);
        TextSettingPanel.SetActive(false);
        SoundSettingPanel.SetActive(false);
        ShortcutSettingPanel.SetActive(false);
        DialougeSettingPanel.SetActive(false);

        SettingWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SettingWindow.SetActive(false);
        if (NextSettingIndex == CurrentSettingIndex) return;

        CurrentSettingIndex = NextSettingIndex;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        GeneralSettingButton.colors = newColor;
        TextSettingButton.colors = newColor;
        SoundSettingButton.colors = newColor;
        ShortcutSettingButton.colors = newColor;
        DialougeSettingButton.colors = newColor;

        GeneralSettingPanel.SetActive(false);
        TextSettingPanel.SetActive(false);
        SoundSettingPanel.SetActive(false);
        ShortcutSettingPanel.SetActive(false);
        DialougeSettingPanel.SetActive(false);

        switch (CurrentSettingIndex)
        {
            case 1:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                GeneralSettingButton.colors = newColor;

                GeneralSettingPanel.SetActive(true);

                break;
            case 2:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                TextSettingButton.colors = newColor;

                TextSettingPanel.SetActive(true);

                break;
            case 3:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                SoundSettingButton.colors = newColor;

                SoundSettingPanel.SetActive(true);

                break;
            case 4:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                ShortcutSettingButton.colors = newColor;

                ShortcutSettingPanel.SetActive(true);

                break;
            case 5:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                DialougeSettingButton.colors = newColor;

                DialougeSettingPanel.SetActive(true);

                break;
        }
    }

    public void SetSettingIndex(int index)
    {
        NextSettingIndex = index;
    }

    public int GetSettingIndex()
    {
        return CurrentSettingIndex;
    }
}
