using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlog : MonoBehaviour
{
    public void DisplayBack()
    {
        DisplayText m_dt = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        GameObject obj = Resources.Load<GameObject>("hondaPrefab/BackLogText");
        GameObject parent = GameObject.Find("BackLogContent");
        for (int i = 0; i < 500; i++)
        {
            GameObject m_backtext = Instantiate(obj) as GameObject;
            if(i < 10)
                m_backtext.GetComponent<Text>().text = "    " + i + "." + m_dt.GetText(i);
            else if (i < 100)
                m_backtext.GetComponent<Text>().text = "  " + i + "." + m_dt.GetText(i);
            else
                m_backtext.GetComponent<Text>().text = i + "." + m_dt.GetText(i);
            m_backtext.transform.SetParent(parent.transform, false);
        }
    }
}
