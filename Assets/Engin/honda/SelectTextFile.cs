using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SelectTextFile : MonoBehaviour
{
    void Start()
    {
        GameObject obj = GameObject.Find("SelectTextFile");
        DirectoryInfo directory = new DirectoryInfo(Application.persistentDataPath + "/Data");
        FileInfo[] files = directory.GetFiles("*.csv");
        string[] afterFile = new string[files.Length];
        obj.GetComponent<Dropdown>().ClearOptions();

        List<string> list = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            afterFile[i] = files[i].Name.Replace(".csv", "");
            Debug.Log(afterFile[i]);
            list.Add(afterFile[i]);
        }
        obj.GetComponent<Dropdown>().AddOptions(list);
        GameObject NowFile = GameObject.Find("NowTextFile");
        NowFile.GetComponent<Image>().GetComponentInChildren<Text>().text = obj.GetComponent<Dropdown>().options[0].text;
    }

    public void NewTextCreate()
    {
        GameObject obj = GameObject.Find("NewTextNameInput");
        string path = Application.persistentDataPath + "/Data/" + obj.GetComponent<InputField>().text + ".csv";
        //変更された情報の書き出し
        StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
        sw.WriteLine("New Text");
        sw.Close();
    }
}
