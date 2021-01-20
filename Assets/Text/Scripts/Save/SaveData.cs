using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string icon_name;
    public string chara_name;
    public string bg_name;
    public string time;
    public string text;
    public string speakertext;
    public int    savetext;
    public string sceneLoadName;
    public int    loadnum;
    public string id;
    public string options;

    public override string ToString()
    {
        return $"{ base.ToString() } { JsonUtility.ToJson(this) }";
    }
}