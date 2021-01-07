using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Const_e;

public class DisplayText : MonoBehaviour
{
    public Text uiText;   // uiTextへの参照
    public Text Nametext;
    public Dropdown m_dropdown;

    private string[] m_stock_text = new string[Engin_Const.line_size];
    private string path;
    private string fileName = "";
    private int serch_num = 0;

    private enum ObjectNumber
    {
        IMG = 0,
        BUTTON
    }
    void Start()
    {
        SelectTextFile tFile = GameObject.Find("SelectTextFile").GetComponent<SelectTextFile>();
        tFile.LoadTextFile();
        LoadText();
        DisplayName(m_dropdown.value);
        SelectText(m_dropdown.value);
        Backlog m_bl = GameObject.Find("BackLog").GetComponent<Backlog>();
        m_bl.DisplayBack();
        InputText m_Input = GameObject.Find("InputMenu").GetComponent<InputText>();
        m_Input.Init();
    }
    void Update()
    {
        SelectLine();
    }
    public void LoadText()
    {
        GameObject NowFile = GameObject.Find("NowTextFile");
        fileName = NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text + ".txt";
        path = Application.persistentDataPath + "/Data/" + fileName;
        StreamReader sr = new StreamReader(path, Encoding.UTF8);
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
        int num = 0;
        //string text_t = new string[Engin_Const.line_size];
        //for(int i=0;i< Engin_Const.line_size; i++)
        //{
        //    if (m_stock_text[i] != null)
        //    {
        //        if (m_stock_text[i].Contains("{") && !m_stock_text[i].Contains("#"))
        //        {
        //            text_t[num] = m_stock_text[i];
        //            text_t[num] = text_t[num].Replace("{", "");
        //            if (m_stock_text[i].Contains("}"))
        //            {
        //                text_t[num] = text_t[num].Replace("}", "");
        //            }
        //            num++;
        //        }
        //    }
        //    else
        //    {
        //        m_stock_text[i] = "";
        //    }
        //}
        if (m_stock_text[index] != null)
        {
            if (m_stock_text[index].Contains("{") && !m_stock_text[index].Contains("#"))
            {
                string text_t = m_stock_text[index];
                text_t = text_t.Replace("{", "");
                if (m_stock_text[index].Contains("}"))
                {
                    text_t = text_t.Replace("}", "");
                }
                uiText.text = text_t;
                num++;
            }
        }
        //uiText.text = text_t[index];
    }
    public void DisplayName(int index)
    {
        string text_t = m_stock_text[index];
        if (m_stock_text[index] != null)
        {
            if (m_stock_text[index].Contains("#"))
            {
                text_t = text_t.Replace("#", "");
                if (m_stock_text[index].Contains("Speaker"))
                {
                    text_t = text_t.Replace("Speaker=", "");
                    Nametext.text = text_t;
                }
                else if (m_stock_text[index].Contains("Background"))
                {
                    text_t = text_t.Replace("Background=", "");
                    GameObject img = GameObject.Find("BackImage");
                    SpriteChange("BackGrounds/" + text_t, img, ObjectNumber.IMG);
                }
                else if (m_stock_text[index].Contains("Ico"))
                {
                    text_t = text_t.Replace("Ico=", "");
                    GameObject img = GameObject.Find("iconImage");
                    SpriteChange("CharaIcons/" + text_t, img, ObjectNumber.IMG);
                }
                else if (m_stock_text[index].Contains("Chara"))
                {
                    text_t = text_t.Replace("Chara=", "");
                    var splitted = text_t.Split('(');
                    var splitted2 = splitted[1].Split(':');
                    splitted2[2] = splitted2[2].Replace(")", "");

                    GameObject img = GameObject.Find("CharacterButton");
                    SpriteChange("Characters/" + splitted[0], img, ObjectNumber.BUTTON);
                    SpriteStatus(img, splitted2[0], splitted2[1], splitted2[2]);
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
        }
    }
    void SpriteStatus(GameObject obj, string sx,string sy,string ss)
    {
        float x, y, scale;
        float.TryParse(sx, out x);
        float.TryParse(sy, out y);
        float.TryParse(ss, out scale);
        obj.GetComponent<Button>().transform.localPosition = new Vector3(x, y, -1.0f);
        obj.GetComponent<Button>().transform.localScale = new Vector3(scale, scale, 1.0f);
    }
    public string GetText(int i)
    {
        return m_stock_text[i];
    }
    private void SelectLineNum()
    {
        if (GameObject.Find("BackLog") != null)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && m_dropdown.value >= 1)
            {
                serch_num--;
                List<int> display_textNum = new List<int>();
                for (int i = 0; i < Engin_Const.line_size; i++)
                {
                    if (m_stock_text[i].Contains("{") && !m_stock_text[i].Contains("#Options"))
                    {
                        display_textNum.Add(i);
                    }
                }
                //m_dropdown.value = serch_num;
                for (int i = 0; i <= display_textNum[serch_num]; i++)
                {
                    DisplayName(i);
                    m_dropdown.value = i;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && m_dropdown.value < Engin_Const.line_size - 1)
            {
                List<int> display_textNum = new List<int>();
                for (int i = 0; i < Engin_Const.line_size; i++)
                {
                    if (m_stock_text[i].Contains("{") && !m_stock_text[i].Contains("#Options"))
                    {
                        display_textNum.Add(i);
                    }
                }
                if (display_textNum.Count - 1 > serch_num)
                {
                    serch_num++;
                    //m_dropdown.value = serch_num;
                    for (int i = 0; i <= display_textNum[serch_num]; i++)
                    {
                        DisplayName(i);
                        m_dropdown.value = i;
                    }
                }
                else
                {
                    m_dropdown.value++;
                }
            }
            SelectText(serch_num);
        }
    }
    private void SelectLine()
    {
        if (GameObject.Find("BackLog") != null)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && m_dropdown.value >= 1)
            {
                m_dropdown.value--;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && m_dropdown.value < Engin_Const.line_size - 1)
            {
                m_dropdown.value++;
            }
            DisplayName(m_dropdown.value);
            SelectText(m_dropdown.value);
        }
    }
    public void ClearText()
    {
        for (int i = 0; i < Engin_Const.line_size; i++)
        {
            m_stock_text[i] = "";
        }
        LoadText();
    }
    public void SetTextLineNum(int index)
    {
        m_dropdown.value = index;
    }
}
