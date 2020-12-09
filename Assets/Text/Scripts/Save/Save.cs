using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    private Image _charaIcoImage;
    private Text _time;
    private Text _comment;
    // Start is called before the first frame update
    void Start()
    {
        DataBank bank = DataBank.Open();
        //Debug.Log("DataBank.Open()");
        //Debug.Log($"save path of bank is { bank.SavePath }");

        SaveData saveData = new SaveData()
        {
            icon_name = gameObject.name,
            time = "YYYY/MM/DD\nah:mm",
            text = "セーブデータがありません"
        };
        //Debug.Log(playerData);

        bank.Store(gameObject.name, saveData);
        //Debug.Log("bank.Store()");

        bank.SaveAll();
        //Debug.Log("bank.SaveAll()");

        //playerData = new SaveData();
        //Debug.Log(playerData);

        saveData = bank.Get<SaveData>(gameObject.name);
        //Debug.Log(saveData);

        //bank.Clear();
        //Debug.Log("bank.Clear()");

        //playerData = bank.Get<PlayerData>("player");
        //Debug.Log(playerData);

        //bank.Load<SaveData>(gameObject.name);
        //Debug.Log("bank.Load()");

        //playerData = bank.Get<SaveData>(gameObject.name);
        //Debug.Log(playerData);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
