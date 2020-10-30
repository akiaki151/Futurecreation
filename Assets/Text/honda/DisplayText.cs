using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

// System.Collections.Genericを定義するとテキストが即時反映されない

public class DisplayText : MonoBehaviour
{
    public Text uiText;   // uiTextへの参照
    public Dropdown m_dropdown;
    public InputField inputtext;
    public SceneHolder m_sh;

    private string[] m_stock_text = new string[500];
    private string path;
    private string fileName = "Data/TextData.csv";

    // Start is called before the first frame update
    void Start()
    {
        LoadText();
    }

    // Update is called once per frame
    void Update()
    {
        SelectText(m_dropdown.value);
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
}
