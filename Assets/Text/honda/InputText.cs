using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class InputText : MonoBehaviour
{
    InputField m_InputText;
    public Dropdown m_dropdown;
    public Dropdown m_CharName;
    public Dropdown m_CharImage;
    private List<string> csvData = new List<string>();
    private string path;
    private string fileName = "Data/TextData.csv";
    private string stock;
    public Button m_button;

    // Start is called before the first frame update
    void Start()
    {
        Load();

        m_InputText = GetComponent<InputField>();
        InitData();

    }

    //InputFieldの初期化
    void InitData()
    {
        m_InputText.text = " ";
        stock = " ";
    }

    //文字列保存
    public void InputData()
    {
        string TextData = m_InputText.text;
        //Debug.Log(TextData);

        FileData(TextData, m_dropdown.value + 1);

        InitData();
    }
    //データの書き換え処理、第一引数:書き換える文字列、第二引数：テキスト番号
    public void FileData(string text_s, int num)
    {
        Load();

        //各情報を格納
        string str = text_s;
        csvData[num - 1] = text_s;

        //変更された情報の書き出し
        StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("Shift_JIS"));
        for (int i = 0;i < 500;i++)
        {
            sw.WriteLine(csvData[i]);
            //Debug.Log(csvData[i]);
        }
        sw.Close();
    }

    public void Load()
    {
        //var rl = Resources.Load("ScenarioData/TextData") as TextAsset;
        //var sr = new StringReader(rl.text);
        path = Application.dataPath + "/" + fileName;
        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Shift_JIS"));
        int index = 0;

        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            csvData.Add(line);
            //Debug.Log(csvData[index]);
            index++;
        }
        sr.Close();
    }

    public void SpeakerName()
    {
        m_InputText.text = stock + "#speaker=" + m_CharName.options[m_CharName.value].text;
        stock = m_InputText.text;
    }
    //public void DeleteSpeakerName()
    //{
    //    m_InputText.text = stock + "#speaker=" + m_CharName.options[m_CharName.value].text;
    //    stock = m_InputText.text;
    //}


    public void CharaImage()
    {
        m_InputText.text = stock + "#chara=" + m_CharImage.captionImage.sprite.name;
        stock = m_InputText.text;
    }
    public void DeleteCharaImage()
    {
        m_InputText.text = stock + "#chara=del_chara";
        stock = m_InputText.text;
    }

    public void Sarchfolder()
    {
        //Image img = GetComponent<Image>();
        DirectoryInfo directory = new DirectoryInfo("Assets/Text/Resources/Image/CharaIcons/");
        FileInfo[] files = directory.GetFiles("*.png");
        //foreach(FileInfo f in files)
        //{
        //    Sprite sprite = Resources.Load<Sprite>("Image/CharaIcons/" + f.Name);
        //    m_button.image.sprite = sprite;
        //}
        Sprite sprite = Resources.Load<Sprite>("Image/CharaIcons/" + files[0].Name);
        m_button.image.sprite = sprite;
        Debug.Log(sprite);
    }
}
