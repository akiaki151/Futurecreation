using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using Const_e;
using UnityEngine.SceneManagement;
//using UnityEngine.

public class InputText : MonoBehaviour
{
    public GameObject m_Log;
    public GameObject m_InputPanel;
    public GameObject m_ElementPanel;
    public GameObject m_TextCreatCanvas;
    public GameObject m_CharacterProperty;
    public GameObject m_Dialogue;
    public GameObject m_SetScene_File;

    private InputField m_InputText;
    private GameObject[] selectbutton = new GameObject[50];
    private Dropdown m_dropdown;
    private Button m_CharImage;
    private Image m_IconImage;
    private Image m_BgImage;
    private string[] csvData = new string[Engin_Const.line_size];
    private string path;

    private string fileName = "Data/csvTest.txt";
    private string stock;
    private int filecount;
    
    public void Init()
    {
        Load();
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
        m_InputText = GameObject.Find("InputTextField").GetComponent<InputField>();
        string TextData = "{" + m_InputText.text + "}";

        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();
        FileData(TextData, m_dropdown.value + 1);

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLogCanvas").GetComponent<Backlog>();
        m_bl.ImportLog();

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
        GameObject NowFile = GameObject.Find("NowTextFile");
        fileName = NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text + ".txt";
        path = Application.persistentDataPath + "/Data/" + fileName;
        StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
        for (int i = 0;i < Engin_Const.line_size; i++)
        {
            sw.WriteLine(csvData[i]);
            Debug.Log(csvData[i]);
        }
        sw.Close();
    }
    public void Load()
    {
        GameObject NowFile = GameObject.Find("NowTextFile");
        fileName = NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text + ".txt";
        path = Application.persistentDataPath + "/Data/" + fileName;
        StreamReader sr = new StreamReader(path, Encoding.UTF8);
        int index = 0;

        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            csvData[index] = line;
            index++;
        }
         sr.Close();
    }
    private void InputTagData(string TagName, string FileName)
    {
        string TextData = TagName + FileName;
        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();
        FileData(TextData, m_dropdown.value + 1);
        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLog").GetComponent<Backlog>();
        m_bl.ImportLog();
    }
    public void SpeakerName()
    {
        Dropdown c_n = GameObject.Find("CharNameDropdown").GetComponent<Dropdown>();
        InputTagData("#Speaker=", c_n.options[c_n.value].text);
    }
    public void CharaImage()
    {
        m_CharImage = GameObject.Find("CharacterButton").GetComponent<Button>();
        InputTagData("#Chara=", m_CharImage.image.sprite.name + "(" + m_CharImage.transform.localPosition.x + ":" + m_CharImage.transform.localPosition.y + ":" + m_CharImage.transform.localScale.x + ")");
    }
    public void DeleteCharaImage()
    {
        m_InputText.text = stock + "#chara=del_chara";
        stock = m_InputText.text;
        m_CharImage.image.sprite = null;
    }
    public void IconImage()
    {
        m_IconImage = GameObject.Find("iconImage").GetComponent<Image>();
        InputTagData("#Ico=", m_IconImage.sprite.name);
    }
    public void DeleteIconImage()
    {
        m_InputText.text = stock + "#ico=del_chara";
        stock = m_InputText.text;
        m_IconImage.sprite = null;
    }
    public void BackImage()
    {
        m_BgImage = GameObject.Find("BackImage").GetComponent<Image>();
        InputTagData("#Background=", m_BgImage.sprite.name);
    }
    public void SEInput()
    {
        //string m_SE = GameObject.Find("SENameFile").GetComponentInChildren<Text>().text;
        string m_SE = GetComponent<LineValue>().GetLine().ToString();
        m_SE = m_SE.Replace("SE : ", "");
        InputTagData("#SE=", m_SE);
    }
    public void BGMInput()
    {
        //string m_BGM = GameObject.Find("BGMNameFile").GetComponentInChildren<Text>().text;
        string m_BGM = GetComponent<LineValue>().GetLine().ToString();
        m_BGM = m_BGM.Replace("BGM : ", "");
        InputTagData("#BGM=", m_BGM);
    }
    public void AnimeInput()
    {
        Dropdown Anime = GameObject.Find("CharaAnimeDropdown").GetComponent<Dropdown>();
        InputTagData("#Action=", Anime.options[Anime.value].text);
    }
    public void SceneSetInput()
    {
        InputField SceneTag = GameObject.Find("InputScene").GetComponent<InputField>();
        string TextData = "#scene=" + SceneTag.text;

        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();
        FileData(TextData, m_dropdown.value + 1);

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLog").GetComponent<Backlog>();
        m_bl.ImportLog();
        SceneTag.text = "";
    }
    public void NextSceneInput()
    {
        InputField NextTag = GameObject.Find("LoadSceneInput").GetComponent<InputField>();
        string TextData = "#Next=" + NextTag.text;

        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();
        FileData(TextData, m_dropdown.value + 1);

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLog").GetComponent<Backlog>();
        m_bl.ImportLog();
        NextTag.text = "";
    }
    public void SetFileInput()
    {
        Dropdown SceneTag = GameObject.Find("SetFileDropdown").GetComponent<Dropdown>();
        string TextData = "#Setscenario=" + SceneTag.options[SceneTag.value].text;

        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();
        FileData(TextData, m_dropdown.value + 1);

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLog").GetComponent<Backlog>();
        m_bl.ImportLog();
    }
    public void NextFileInput()
    {
        Dropdown SceneTag = GameObject.Find("LoadFileDropdown").GetComponent<Dropdown>();
        string TextData = "#Getscenario=" + SceneTag.options[SceneTag.value].text;

        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();
        FileData(TextData, m_dropdown.value + 1);

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLog").GetComponent<Backlog>();
        m_bl.ImportLog();
    }
    public void OpenBackLog()
    {
        if (!m_Log.activeSelf)
            m_Log.SetActive(true);
    }
    private void SarchImagefolder(string prefabName,string sarch_path)
    {
        if (GameObject.Find("Content").transform.childCount != 0)
        {
            DeleteButton();
        }
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/" + prefabName);
        GameObject parent = GameObject.Find("Content");
        DirectoryInfo directory = new DirectoryInfo("Assets/Text/Resources/Image/" + sarch_path + "/");
        FileInfo[] files = new FileInfo[50];
        //if(sarch_path.Contains("CharaIcons") || sarch_path.Contains("Characters"))
            files = directory.GetFiles("*.png");
        //else
        //    files = directory.GetFiles("*.jpg");
        string[] afterFile = new string[files.Length];
        filecount = files.Length;
        for (int i = 0; i < files.Length; i++)
        {
            //if (sarch_path.Contains("CharaIcons") || sarch_path.Contains("Characters"))
                afterFile[i] = files[i].Name.Replace(".png", "");
            //else if (sarch_path.Contains("BackGrounds"))
            //    afterFile[i] = files[i].Name.Replace(".jpg", "");
            Sprite sprite = Resources.Load<Sprite>("Image/" + sarch_path + "/" + afterFile[i]);
            selectbutton[i] = Instantiate(obj) as GameObject;
            selectbutton[i].GetComponent<Button>().image.sprite = sprite;
            selectbutton[i].transform.SetParent(parent.transform, false);
        }
    }
    private void SarchSoundfolder(string prefabName, string sarch_path)
    {
        if (GameObject.Find("Content").transform.childCount != 0)
        {
            DeleteButton();
        }
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/" + prefabName);
        GameObject parent = GameObject.Find("Content");
        DirectoryInfo directory = new DirectoryInfo("Assets/Text/Sounds/" + sarch_path + "/");
        FileInfo[] files = directory.GetFiles("*.mp3");
        string[] afterFile = new string[files.Length];
        filecount = files.Length;
        Debug.Log(filecount);
        for (int i = 0; i < files.Length; i++)
        {
            afterFile[i] = files[i].Name.Replace(".mp3", "");
            selectbutton[i] = Instantiate(obj) as GameObject;
            selectbutton[i].GetComponent<Button>().GetComponentInChildren<Text>().text = afterFile[i];
            selectbutton[i].transform.SetParent(parent.transform, false);
            selectbutton[i].GetComponent<LineValue>().SetValue(i);
        }
    }
    public void Sarchiconfolder()
    {
        SarchImagefolder("IConSelectButton", "CharaIcons");
    }
    public void SarchCharcterfolder()
    {
        SarchImagefolder("CharacterSelectButton", "Characters");
    }
    public void SarchBackImagefolder()
    {
        SarchImagefolder("BackimgSelectButton", "BackGrounds");
    }
    public void SarchSEfolder()
    {
        SarchSoundfolder("SESelectButton", "SE");
    }
    public void SarchBGMfolder()
    {
        SarchSoundfolder("BGMSelectButton", "BGM");
    }
    public void DeleteButton()
    {
        for (int i = 0; i < filecount; i++)
        {
            Debug.Log(GameObject.Find("Content").transform.GetChild(i).gameObject);
            Destroy(GameObject.Find("Content").transform.GetChild(i).gameObject);
        }
        //m_ImageBar.SetActive(false);
    }
    public void CloseLog()
    {
        m_Log.SetActive(false);
    }
    public void UI_DisplaySwitch()
    {
        if(m_ElementPanel.activeSelf)
        {
            m_ElementPanel.SetActive(false);
            m_Log.SetActive(false);
        }
        else
        {
            m_ElementPanel.SetActive(true);
            m_Log.SetActive(true);

        }
    }
    public void OpenInputPanel()
    {
        if (!m_InputPanel.activeSelf)
            m_InputPanel.SetActive(true);
    }
    public void CloseInputPanel()
    {
        m_InputPanel.SetActive(false);
    }
    public void OpenNewTextCanvas()
    {
        if (!m_TextCreatCanvas.activeSelf)
            m_TextCreatCanvas.SetActive(true);
    }
    public void CloseNewTextCanvas()
    {
        m_TextCreatCanvas.SetActive(false);
    }
    public void CheangeFile()
    {
        GameObject obj = GameObject.Find("SelectTextFile");
        GameObject NowFile = GameObject.Find("NowTextFile");
        NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text = obj.GetComponent<Dropdown>().options[obj.GetComponent<Dropdown>().value].text;
    }
    public void NewLine()
    {
        Load();

        int num = 0;
        //各情報を格納
        string str = "";
        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();

        //変更された情報の書き出し
        GameObject NowFile = GameObject.Find("NowTextFile");
        fileName = NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text + ".txt";
        path = Application.persistentDataPath + "/Data/" + fileName;
        StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
        for (int i = 0; i < Engin_Const.line_size; i++)
        {
            if (m_dropdown.value != i)
            {
                sw.WriteLine(csvData[num]);
                num++;
            }
            else if (m_dropdown.value == i)
                sw.WriteLine(str);

        }
        sw.Close();

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLogCanvas").GetComponent<Backlog>();
        m_bl.ImportLog();
    }

    public void DeleteLine()
    {
        Load();

        int num = 0;
        //各情報を格納
        string str = "";
        m_dropdown = GameObject.Find("TextNum").GetComponentInChildren<Dropdown>();

        //変更された情報の書き出し
        GameObject NowFile = GameObject.Find("NowTextFile");
        fileName = NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text + ".txt";
        path = Application.persistentDataPath + "/Data/" + fileName;
        StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
        for (int i = 0; i < Engin_Const.line_size; i++)
        {
            if (m_dropdown.value != i)
            {
                sw.WriteLine(csvData[num]);
                num++;
            }
            else if (m_dropdown.value == i)
            {
                if (csvData[m_dropdown.value] != "")
                {
                    sw.WriteLine(str);
                    num++;
                }
                else if (csvData[m_dropdown.value] == "")
                {
                    var list = new List<string>();
                    list.AddRange(csvData);
                    list.RemoveAt(m_dropdown.value);
                    csvData = list.ToArray();
                }
            }
        }
        sw.Close();

        DisplayText m_tc = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_tc.ClearText();
        Backlog m_bl = GameObject.Find("BackLogCanvas").GetComponent<Backlog>();
        m_bl.ImportLog();
    }
    public void OpenCharacterProperty()
    {
        if (!m_CharacterProperty.activeSelf)
            m_CharacterProperty.SetActive(true);
    }
    public void CloseCharacterProperty()
    {
        m_CharacterProperty.SetActive(false);
    }
    public void OpenDialog()
    {
        if(!m_Dialogue.activeSelf)
        {
            m_Dialogue.SetActive(true);
        }
    }
    public void CloseDialog()
    {
        m_Dialogue.SetActive(false);
    }
    public void OpenSetScene()
    {
        if (!m_SetScene_File.activeSelf)
            m_SetScene_File.SetActive(true);
    }
    public void CloseSetScene()
    {
        m_SetScene_File.SetActive(false);
    }
    public void SceneDebug()
    {
        SceneManager.LoadSceneAsync("Scenes/debug/Text");
    }
}
