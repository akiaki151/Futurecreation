using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public List<int> statusList;

    public override string ToString()
    {
        return $"{ base.ToString() } { JsonUtility.ToJson(this) }";
    }
}