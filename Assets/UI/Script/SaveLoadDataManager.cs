using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadDataManager : MonoBehaviour
{
    [SerializeField] private GameObject SaveLoadWindow;
    //[SerializeField] private GameObject GeneralSettingPanel, TextSettingPanel, SoundSettingPanel, ShortcutSettingPanel, DialougeSettingPanel, ControlSettingPanel;

    [SerializeField] private Button SaveButton, LoadButton, QuickLoadButton, ToTitleButton, ToGameButton;
    [SerializeField] private Button Page_1, Page_2, Page_3, Page_4, Page_5, Page_6, Page_7, Page_8;
    [SerializeField] private Button CopySelectedDataButton, MoveSelectedDataButton;

    [SerializeField] private Text PageTitle;

    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    private enum SaveLoadIndex
    {
        NONE = 0,
        SAVE,
        LOAD,
        QUICKLOAD,
        TOTITLE,
        TOGAME
    }

    private int CurrentSaveLoadIndex;
    private int NextSaveLoadIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
