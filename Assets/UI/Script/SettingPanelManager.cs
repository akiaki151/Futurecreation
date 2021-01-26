using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingWindow;
    [SerializeField] private GameObject GeneralSettingPanel, ShortcutSettingPanel;

    [SerializeField] private Button GeneralSettingButton, ShortcutSettingButton;

    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    private enum SettingIndex
    {
        NONE = 0,
        GENERAL,
        SHORTCUT,
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
        ShortcutSettingButton.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        GeneralSettingButton.colors = newColor;

        GeneralSettingPanel.SetActive(true);
        ShortcutSettingPanel.SetActive(false);

        SettingWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NextSettingIndex == CurrentSettingIndex) return;

        CurrentSettingIndex = NextSettingIndex;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        GeneralSettingButton.colors = newColor;
        ShortcutSettingButton.colors = newColor;

        GeneralSettingPanel.SetActive(false);
        ShortcutSettingPanel.SetActive(false);

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
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                ShortcutSettingButton.colors = newColor;

                ShortcutSettingPanel.SetActive(true);

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
