using UnityEngine;
using UnityEngine.UI;
using System;

public class Save : MonoBehaviour
{
    private Image _charaIcoImage;
    private Text _Text;
    private Text _time;
    private Text _comment;
    private SaveData saveData;
    private DataBank bank;
    private GameObject canvas;

    void Start()
    {
        bank = DataBank.Open();
        //Debug.Log($"save path of bank is { bank.SavePath }");

        //セーブデータが存在しないならば作成せよのIF文
        bank.Load<SaveData>(gameObject.name);
        saveData = bank.Get<SaveData>(gameObject.name);
        if (saveData == null)
        {
            saveData = new SaveData()
            {
                icon_name = "silhouette",
                time = "YYYY/MM/DD\nah:mm",
                text = "セーブデータがありません"
            };

            bank.Store(gameObject.name, saveData);

            bank.SaveAll();

            saveData = bank.Get<SaveData>(gameObject.name);
            //bank.Clear();
        }


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
        _charaIcoImage.sprite = Resources.Load<Sprite>("Image/CharaIcons/" + saveData.icon_name);
        _time.text = saveData.time;
        _comment.text = saveData.text;
    }

    void Update()
    {
    }

    public void OnClick()
    {
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterIcon")
            {
                _charaIcoImage = child.gameObject.GetComponent<Image>();
            }
        }
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "TextWindow")
            {
                foreach (Transform child2 in child.transform)
                {
                    if (child2.name == "TextPanel")
                    {
                        foreach (Transform child3 in child2.transform)
                        {
                            if (child3.name == "Text")
                            {
                                _Text = child3.gameObject.GetComponent<Text>();
                            }
                        }
                    }
                }
            }
        }

        DateTime TodayNow = DateTime.Now;
        saveData = new SaveData()
        {
            icon_name = _charaIcoImage.sprite.name,
            time = TodayNow.Year.ToString() + "/" + TodayNow.Month.ToString() + "/" + TodayNow.Day.ToString() + "\n" + DateTime.Now.ToShortTimeString(),
            text = _Text.text
        };

        bank.Store(gameObject.name, saveData);
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
        _charaIcoImage.sprite = Resources.Load<Sprite>("Image/CharaIcons/" + saveData.icon_name);
        _time.text = saveData.time;
        _comment.text = saveData.text;
    }
}

