using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class LoadFile : MonoBehaviour
{
    void Start()
    {
        GameObject obj1 = GameObject.Find("SetFileDropdown");
        GameObject obj2 = GameObject.Find("LoadFileDropdown");
        DirectoryInfo directory = new DirectoryInfo(Application.persistentDataPath + "/Data");
        FileInfo[] files = directory.GetFiles("*.txt");
        string[] afterFile = new string[files.Length];
        obj1.GetComponent<Dropdown>().ClearOptions();
        obj2.GetComponent<Dropdown>().ClearOptions();

        List<string> list = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            afterFile[i] = files[i].Name.Replace(".txt", "");
            list.Add(afterFile[i]);
        }
        obj1.GetComponent<Dropdown>().AddOptions(list);
        obj2.GetComponent<Dropdown>().AddOptions(list);
    }

}
