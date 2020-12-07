using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const_e;

public class Backlog : MonoBehaviour
{
    private GameObject[] m_backtext = new GameObject[Engin_Const.line_size];
    public void DisplayBack()
    {
        DisplayText m_dt = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/BackLogText");
        GameObject parent = GameObject.Find("BackLogContent");
        for (int i = 0; i < Engin_Const.line_size; i++)
        {
            m_backtext[i] = Instantiate(obj) as GameObject;
            if(i < 9)
                m_backtext[i].GetComponentInChildren<Text>().text = "    " + (i + 1) + "." + m_dt.GetText(i);
            else if (i < 99)
                m_backtext[i].GetComponentInChildren<Text>().text = "  " + (i + 1) + "." + m_dt.GetText(i);
            else
                m_backtext[i].GetComponentInChildren<Text>().text = (i + 1) + "." + m_dt.GetText(i);
            m_backtext[i].transform.SetParent(parent.transform, false);
            m_backtext[i].GetComponent<LineValue>().SetValue(i);
        }
    }

    public void ImportLog()
    {
        DisplayText m_dt = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        for (int i = 0; i < Engin_Const.line_size; i++)
        {
            if (i < 9)
                m_backtext[i].GetComponentInChildren<Text>().text = "    " + (i + 1) + "." + m_dt.GetText(i);
            else if (i < 99)
                m_backtext[i].GetComponentInChildren<Text>().text = "  " + (i + 1) + "." + m_dt.GetText(i);
            else
                m_backtext[i].GetComponentInChildren<Text>().text = (i + 1) + "." + m_dt.GetText(i);
            m_backtext[i].GetComponent<LineValue>().SetValue(i);
        }
    }

}
