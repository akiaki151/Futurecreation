using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class DisplayText : MonoBehaviour
{
    public Text uiText;   // uiTextへの参照
    public Text Nametext;
    public Dropdown m_dropdown;
    public InputField inputtext;

    private string[] m_stock_text = new string[500];
    private string path;
    private string fileName = "Data/TextData.csv";

    private enum ObjectNumber
    {
        IMG = 0,
        BUTTON
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadText();
        DisplayName(m_dropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        SelectText(m_dropdown.value);

        if (Input.GetKeyDown(KeyCode.UpArrow) && m_dropdown.value >= 1)
        {
            m_dropdown.value--;
            DisplayName(m_dropdown.value);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && m_dropdown.value < 499)
        {
            m_dropdown.value++;
            DisplayName(m_dropdown.value);
        }
    }

    public void LoadText()
    {
        path = Application.dataPath + "/" + fileName;
        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Shift_JIS"));
        int num = 0;
        // 行がnullじゃない間(つまり次の行がある場合は)、処理をする
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            m_stock_text[num] = line;
            num++;
        }
        // StreamReaderを閉じる
        sr.Close();
    }

    public void SelectText(int index)
    {
        uiText.text = m_stock_text[index];
    }

    public void DisplayName(int index)
    {
        string text_t = m_stock_text[index];
        if (m_stock_text[index] != null)
        {
            if (m_stock_text[index].Contains("#"))
            {
                text_t = text_t.Replace("#", "");
                if (m_stock_text[index].Contains("speaker"))
                {
                    text_t = text_t.Replace("speaker=", "");
                    Nametext.text = text_t;
                }
                else if (m_stock_text[index].Contains("background"))
                {
                    text_t = text_t.Replace("background=", "");
                    GameObject img = GameObject.Find("BackImage");
                    SpriteChange("BackGrounds/" + text_t, img, ObjectNumber.IMG);
                }
                else if (m_stock_text[index].Contains("ico"))
                {
                    text_t = text_t.Replace("ico=", "");
                    GameObject img = GameObject.Find("iconButton");
                    SpriteChange("CharaIcons/" + text_t, img, ObjectNumber.BUTTON);
                }
                else if (m_stock_text[index].Contains("chara"))
                {
                    text_t = text_t.Replace("chara=", "");
                    GameObject img = GameObject.Find("CharacterButton");
                    SpriteChange("Characters/" + text_t, img, ObjectNumber.BUTTON);
                }
            }
        }
    }

    void SpriteChange(string t,GameObject obj, ObjectNumber number)
    {
        Sprite load_t = Resources.Load<Sprite>("Image/" + t);
        if (number == ObjectNumber.IMG)
            obj.GetComponent<Image>().sprite = load_t;
        if (number == ObjectNumber.BUTTON)
        {
            obj.GetComponent<Button>().image.sprite = load_t;
            Debug.Log(obj.GetComponent<Button>().image.sprite);
        }
    }

    public string GetText(int i)
    {
        return m_stock_text[i];
    }
}
