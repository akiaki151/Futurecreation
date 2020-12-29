using UnityEngine;
using UnityEngine.UI;
using System;

public class Save : MonoBehaviour
{
    private Image _charaIcoImage;
    private Image _bgImage;
    private Image _charaImage;
    private Text _Text;
    private Text _time;
    private Text _comment;
    private Text _speakerText;
    private SaveData saveData;
    private DataBank bank;
    private GameObject canvas;
    public int Savetext;
    private GameController _gc;
    private SaveLoadDataManager _sldm;

    void Start()
    {
        _gc = GameObject.Find("GameController").GetComponent<GameController>();
        _sldm = GameObject.Find("SaveLoadWindow").GetComponent<SaveLoadDataManager>();
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
                chara_name= "del_chara",
                time = "YYYY/MM/DD\nah:mm",
                text = "セーブデータがありません",
                sceneLoadName = "",
                loadnum = 0,
                id = ""
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

    public void SaveChangePanel()
    {
        bank = DataBank.Open();

        //セーブデータが存在しないならば作成せよのIF文
        bank.Load<SaveData>(gameObject.name);
        saveData = bank.Get<SaveData>(gameObject.name);
        if (saveData == null)
        {
            saveData = new SaveData()
            {
                icon_name = "silhouette",
                chara_name = "del_chara",
                time = "YYYY/MM/DD\nah:mm",
                text = "セーブデータがありません",
                sceneLoadName = "",
                loadnum = 0,
                id = ""
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

    public void OnClickSaveLoad()
    {
        Debug.Log(_sldm.CurrentSaveLoadIndex);
        if (_sldm.CurrentSaveLoadIndex == 2)
        {
            LoadOn();
            return;
        }
        else if (_sldm.CurrentSaveLoadIndex == 0|| _sldm.CurrentSaveLoadIndex == 1)
        {
            SaveOn();
            return;
        }
       
    }

    private void SaveOn()
    {
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterIcon")
            {
                _charaIcoImage = child.gameObject.GetComponent<Image>();
                foreach(Transform child2 in child.transform)
                {
                    if (child2.name == "CharacterName")
                    {
                        foreach (Transform child3 in child2.transform)
                        {
                            if (child3.name == "Text")
                            {
                                _speakerText = child3.gameObject.GetComponent<Text>();
                            }
                        }
                    }
                }
            }
        }
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterImage")
            {
                _charaImage = child.gameObject.GetComponent<Image>();
            }
        }
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "Background")
            {
                _bgImage = child.gameObject.GetComponent<Image>();
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
            chara_name  = _charaImage.sprite.name,
            speakertext = _speakerText.text,
            bg_name     = _bgImage.sprite.name,
            icon_name   = _charaIcoImage.sprite.name,
            time = TodayNow.Year.ToString() + "/" + TodayNow.Month.ToString() + "/" + TodayNow.Day.ToString() + "\n" + DateTime.Now.ToShortTimeString(),
            text = _Text.text,
            savetext = Savetext,
            sceneLoadName = _gc.Sc.sceneLoadName,
            loadnum = _gc.Sc.loadnum,
            id = _gc.Sc.ID
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

    private void LoadOn()
    {
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterIcon")
            {
                _charaIcoImage = child.gameObject.GetComponent<Image>();
                foreach (Transform child2 in child.transform)
                {
                    if (child2.name == "CharacterName")
                    {
                        foreach (Transform child3 in child2.transform)
                        {
                            if (child3.name == "Text")
                            {
                                _speakerText = child3.gameObject.GetComponent<Text>();
                            }
                        }
                    }
                }
            }
        }
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterImage")
            {
                _charaImage = child.gameObject.GetComponent<Image>();
            }
        }
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "Background")
            {
                _bgImage = child.gameObject.GetComponent<Image>();
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
        _speakerText.text = saveData.speakertext;
        _charaIcoImage.sprite = Resources.Load<Sprite>("Image/CharaIcons/" + saveData.icon_name);
        _charaImage.sprite    = Resources.Load<Sprite>("Image/Characters/" + saveData.chara_name);
        _bgImage.sprite       = Resources.Load<Sprite>("Image/BackGrounds/" + saveData.bg_name);
        _Text.text = saveData.text;

        bank = DataBank.Open();
        bank.Load<SaveData>(gameObject.name);
        saveData = bank.Get<SaveData>(gameObject.name);
        _gc.Sc.LoadScene(saveData.sceneLoadName, saveData.loadnum, saveData.id);
    }
}
