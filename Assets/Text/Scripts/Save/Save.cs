using UnityEngine;
using UnityEngine.UI;
using System;

public class Save : MonoBehaviour
{
    private Image _charaIcoImage;
    private Text _time;
    private Text _comment;
    private SaveData saveData;
    private DataBank bank;
   
    void Start()
    {
        bank = DataBank.Open();
        //Debug.Log($"save path of bank is { bank.SavePath }");

        //セーブデータが存在しないならば作成せよのIF文
        saveData = bank.Get<SaveData>(gameObject.name);
        bank.Load<SaveData>(gameObject.name);
        if(saveData == null)
        {
            saveData = new SaveData()
            {
                icon_name = gameObject.name,
                time = "YYYY/MM/DD\nah:mm",
                text = "セーブデータがありません"
            };

            bank.Store(gameObject.name, saveData);
            //Debug.Log("bank.Store()");

            bank.SaveAll();

            saveData = bank.Get<SaveData>(gameObject.name);
            //Debug.Log(saveData);

            //bank.Clear();
        }


        foreach (Transform child in this.transform)
        {
            if (child.name == "Image")
            {
                _charaIcoImage = child.gameObject.GetComponent<Image>();
            }
            else if(child.name == "Time")
            {
                _time = child.gameObject.GetComponent<Text>();
            }
            else if (child.name == "Comment")
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.name == "Placeholder")
                    {
                        _comment = child2.gameObject.GetComponent<Text>();
                    }
                }
            }
        }
        _charaIcoImage.sprite = Resources.Load<Sprite>("Image/CharaIcons/silhouette");
        _time.text = saveData.time;
        _comment.text = saveData.text;
    }

    void Update()
    {
    }

    public void OnClick()
    {
        DateTime TodayNow = DateTime.Now;
        saveData = new SaveData()
        {
            icon_name = gameObject.name,
            time = TodayNow.Year.ToString() + "/" + TodayNow.Month.ToString() + "/" + TodayNow.Day.ToString() + "\n" + DateTime.Now.ToShortTimeString(),
            text = "セーブデータがありません"
        };

        bank.Store(gameObject.name, saveData);
        //Debug.Log("bank.Store()");

        bank.SaveAll();
        saveData = bank.Get<SaveData>(gameObject.name);

        foreach (Transform child in this.transform)
        {
            if (child.name == "Image")
            {
                _charaIcoImage = child.gameObject.GetComponent<Image>();
            }
            else if (child.name == "Time")
            {
                _time = child.gameObject.GetComponent<Text>();
            }
            else if (child.name == "Comment")
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.name == "Placeholder")
                    {
                        _comment = child2.gameObject.GetComponent<Text>();
                    }
                }
            }
        }
        _charaIcoImage.sprite = Resources.Load<Sprite>("Image/CharaIcons/d_ico");
        _time.text = saveData.time;
        _comment.text = saveData.text;
    }
}
