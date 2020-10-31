using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class InputText : MonoBehaviour
{
    const int line_size = 500;
    InputField m_InputText;
    GameObject[] iconselectbutton = new GameObject[50];
    public Dropdown m_dropdown;
    public Dropdown m_CharName;
    public Button m_CharImage;
    public Button m_IconImage;
    public Image m_BgImage;
    public GameObject m_ImageBar;
    public GameObject m_Log;
    //private List<string> csvData = new List<string>();
    private string[] csvData = new string[line_size];
    private string path;
    private string fileName = "Data/TextData.csv";
    private string stock;
    private int filecount = 0;

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
        m_InputText.text = "";
        stock = "";
    }

    //文字列保存
    public void InputData()
    {
        string TextData = m_InputText.text;

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
        for (int i = 0;i < line_size; i++)
        {
            sw.WriteLine(csvData[i]);
            Debug.Log(csvData[i]);
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
            csvData[index] = line;
            index++;
        }
         sr.Close();
    }

    public void SpeakerName()
    {
        m_InputText.text = stock + "#speaker=" + m_CharName.options[m_CharName.value].text;
        stock = m_InputText.text;
    }
    public void CharaImage()
    {
        m_InputText.text = stock + "#chara=" + m_CharImage.image.sprite.name;
        stock = m_InputText.text;
        DeleteButton();
    }
    public void DeleteCharaImage()
    {
        m_InputText.text = stock + "#chara=del_chara";
        stock = m_InputText.text;
        m_CharImage.image.sprite = null;
    }
    public void IconImage()
    {
        m_InputText.text = stock + "#ico=" + m_IconImage.image.sprite.name;
        stock = m_InputText.text;
        DeleteButton();
    }
    public void DeleteIconImage()
    {
        m_InputText.text = stock + "#ico=del_chara";
        stock = m_InputText.text;
        m_IconImage.image.sprite = null;
    }
    public void BackImage()
    {
        m_InputText.text = stock + "#background=" + m_BgImage.sprite.name;
        stock = m_InputText.text;
        DeleteButton();
    }
    public void OpenBar()
    {
        if (!m_ImageBar.activeSelf)
            m_ImageBar.SetActive(true);
    }
    public void OpenBackLog()
    {
        if (!m_Log.activeSelf)
            m_Log.SetActive(true);
    }
    public void Sarchiconfolder()
    {
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/IConSelectButton");
        GameObject parent = GameObject.Find("Content");
        DirectoryInfo directory = new DirectoryInfo("Assets/Text/Resources/Image/CharaIcons/");
        FileInfo[] files = directory.GetFiles("*.png");
        string[] afterFile = new string[files.Length];
        filecount = files.Length;
        if (parent.transform.childCount == 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                afterFile[i] = files[i].Name.Replace(".png", "");
                Sprite sprite = Resources.Load<Sprite>("Image/CharaIcons/" + afterFile[i]);
                iconselectbutton[i] = Instantiate(obj) as GameObject;
                iconselectbutton[i].GetComponent<Button>().image.sprite = sprite;
                iconselectbutton[i].transform.SetParent(parent.transform, false);
            }
        }
    }
    public void SarchCharcterfolder()
    {
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/CharacterSelectButton");
        GameObject parent = GameObject.Find("Content");
        DirectoryInfo directory = new DirectoryInfo("Assets/Text/Resources/Image/Characters/");
        FileInfo[] files = directory.GetFiles("*.png");
        string[] afterFile = new string[files.Length];
        filecount = files.Length;
        if (parent.transform.childCount == 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                afterFile[i] = files[i].Name.Replace(".png", "");
                Sprite sprite = Resources.Load<Sprite>("Image/Characters/" + afterFile[i]);
                iconselectbutton[i] = Instantiate(obj) as GameObject;
                iconselectbutton[i].GetComponent<Button>().image.sprite = sprite;
                iconselectbutton[i].transform.SetParent(parent.transform, false);
            }
        }
    }
    public void SarchBackImagefolder()
    {
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/BackimgSelectButton");
        GameObject parent = GameObject.Find("Content");
        DirectoryInfo directory = new DirectoryInfo("Assets/Text/Resources/Image/BackGrounds/");
        FileInfo[] files = directory.GetFiles("*.jpg");
        string[] afterFile = new string[files.Length];
        filecount = files.Length;
        if (parent.transform.childCount == 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                afterFile[i] = files[i].Name.Replace(".jpg", "");
                Sprite sprite = Resources.Load<Sprite>("Image/BackGrounds/" + afterFile[i]);
                iconselectbutton[i] = Instantiate(obj) as GameObject;
                iconselectbutton[i].GetComponent<Button>().image.sprite = sprite;
                iconselectbutton[i].transform.SetParent(parent.transform, false);
            }
        }
    }
    public void DeleteButton()
    {
        for (int i = 0; i < filecount; i++)
        {
            Debug.Log(iconselectbutton);
            Destroy(iconselectbutton[i]);
        }
        m_ImageBar.SetActive(false);
    }
    public void CloseLog()
    {
        m_Log.SetActive(false);
    }
}
