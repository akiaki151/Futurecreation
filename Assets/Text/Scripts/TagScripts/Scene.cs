﻿using System.Collections.Generic;
using UnityEngine;

public class Scene 
{
    public string ID { get; private set; }
    public List<string> Lines { get; private set; } = new List<string>();
    public int Index { get; set; } = 0;

    public Scene(string ID = "")
    {
        this.ID = ID;
    }

    public Scene Clone()
    {
        return new Scene(ID)
        {
            Index = 0,
            Lines = new List<string>(Lines)
        };
    }

    public bool IsFinished()
    {
        return Index >= Lines.Count;
    }

    public string GetCurrentLine()
    {
        return Lines[Index];
    }

    public void LoadLine(int cnt)
    {
        Index = cnt;
        GetCurrentLine();
    }

    public void GoNextLine()
    {
        Index++;
    }
}