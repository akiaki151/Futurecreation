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
    private InputField Comment;

    // Start is called before the first frame update
    void Start()
    {
        PageManager = GameObject.Find("Page").GetComponent<SaveLoadPagemanager>();
        PageNum = PageManager.GetCurrentPage();
        DataNumber = (PageNum - 1) * 12 + BaseDataNumber;
        this.name = "DataPanel_" + DataNumber.ToString();
        transform.FindChild("Number").GetComponent<Text>().text = "No." + DataNumber.ToString();

        if (SaveData)
        {
            transform.FindChild("Image").GetComponent<Image>().sprite = IsSaveData;
        }
        else
        {
            transform.FindChild("Image").GetComponent<Image>().sprite = NoSaveData;
            transform.FindChild("Time").GetComponent<Text>().text = "";
            transform.FindChild("ChapterName").GetComponent<Text>().text = "";
            transform.FindChild("Comment").FindChild("Text").GetComponent<Text>().text = "";
            transform.FindChild("Comment").FindChild("Placeholder").GetComponent<Text>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PageNum != PageManager.GetCurrentPage())
        {
            PageNum = PageManager.GetCurrentPage();
            DataNumber = (PageNum - 1) * 12 + BaseDataNumber;
            this.name = "DataPanel_" + DataNumber.ToString();
            transform.FindChild("Number").GetComponent<Text>().text = "No." + DataNumber.ToString();
        }
    }
}