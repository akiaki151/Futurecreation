using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingWindow;
    [SerializeField] private GameObject GeneralSettingPanel, TextSettingPanel, SoundSettingPanel, ShortcutSettingPanel, DialougeSettingPanel, ControlSettingPanel;

    [SerializeField] private Button GeneralSettingButton, TextSettingButton, SoundSettingButton, ShortcutSettingButton, DialougeSettingButton, ControlSettingButton;

    [SerializeField] private Text PageTitle;

    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    private enum SettingIndex
    {
        NONE = 0,
        GENERAL,
        TEXT,
        SOUND,
        SHORTCUT,
        DIALOUGE,
        CONTROL
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
        ControlSettingButton.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        GeneralSettingButton.colors = newColor;
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
        ControlSettingButton.colors = newColor;

        GeneralSettingPanel.SetActive(false);
        TextSettingPanel.SetActive(false);
        SoundSettingPanel.SetActive(false);
        ShortcutSettingPanel.SetActive(false);
        DialougeSettingPanel.SetActive(false);
        ControlSettingPanel.SetActive(false);

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

                PageTitle.text = "簡単設定";

                GeneralSettingPanel.SetActive(true);

                break;
            case 2:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                TextSettingButton.colors = newColor;

                PageTitle.text = "テキスト設定";

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

                PageTitle.text = "サウンド設定";

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

                PageTitle.text = "ショートカット設定";

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

                PageTitle.text = "ダイアログ設定";

                DialougeSettingPanel.SetActive(true);

                break;

            case 6:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                ControlSettingButton.colors = newColor;

                PageTitle.text = "ダイアログ設定";

                ControlSettingPanel.SetActive(true);

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
