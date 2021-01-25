using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadScreenDataPanel : MonoBehaviour
{
    [SerializeField] private int BaseDataNumber;
    [SerializeField] private int DataNumber;
    [SerializeField] private bool SaveData;
    [SerializeField] private Sprite NoSaveData;
    [SerializeField] private Sprite IsSaveData;
    [SerializeField] private string Time;
    [SerializeField] private string ChapterName;
    [SerializeField] private SaveLoadPagemanager PageManager;
    [SerializeField] private int PageNum;
    [SerializeField] private Save save;
    private InputField Comment;

    // Start is called before the first frame update
    void Start()
    {
        PageManager = GameObject.Find("Page").GetComponent<SaveLoadPagemanager>();
        save = this.GetComponent<Save>();
        PageNum = PageManager.GetCurrentPage();
        DataNumber = (PageNum - 1) * 12 + BaseDataNumber;
        this.name = "DataPanel_" + DataNumber.ToString();
        transform.Find("Number").GetComponent<Text>().text = "No." + DataNumber.ToString();

        if (SaveData)
        {
            transform.Find("Image").GetComponent<Image>().sprite = IsSaveData;
        }
        else
        {
            transform.Find("Image").GetComponent<Image>().sprite = NoSaveData;
            transform.Find("Time").GetComponent<Text>().text = "";
            transform.Find("ChapterName").GetComponent<Text>().text = "";
            transform.Find("Comment").Find("Text").GetComponent<Text>().text = "";
            transform.Find("Comment").Find("Placeholder").GetComponent<Text>().text = "";
        }

        save.SaveChangePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (PageNum != PageManager.GetCurrentPage())
        {
            PageNum = PageManager.GetCurrentPage();
            DataNumber = (PageNum - 1) * 12 + BaseDataNumber;
            this.name = "DataPanel_" + DataNumber.ToString();
            transform.Find("Number").GetComponent<Text>().text = "No." + DataNumber.ToString();
            save.SaveChangePanel();
        }
    }

    void SetSaveData(bool value)
    {
        SaveData = value;
    }
}