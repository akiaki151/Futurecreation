using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineValue : MonoBehaviour
{
    public int value = 0;
    public void SetValue(int index)
    {
        value = index;
    }

    public void LineCheange()
    {
        DisplayText m_dt = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        m_dt.SetTextLineNum(value);
    }
}
