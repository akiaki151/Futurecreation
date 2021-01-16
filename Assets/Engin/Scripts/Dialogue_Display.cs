using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const_e;

public class Dialogue_Display : MonoBehaviour
{
    private void Start()
    {
        DialogueText();
    }

    private void DialogueText()
    {
        string stock = "";
        string speaker = "";
        DisplayText m_dt = GameObject.Find("TextContlloer").GetComponent<DisplayText>();
        for (int i = 0; i < Engin_Const.line_size; i++)
        {

            if(m_dt.GetText(i).Contains("#Speaker"))
            {
                if (m_dt.GetText(i) != stock)
                {
                    speaker = m_dt.GetText(i);
                    stock = m_dt.GetText(i);
                    speaker = speaker.Replace("#Speaker=", "");
                }
            }

            if (m_dt.GetText(i).Contains("{"))
            {
                 string dialogue = m_dt.GetText(i);
                dialogue = dialogue.Replace("{", "");
                dialogue = dialogue.Replace("}", "");
                GameObject.Find("LogText").GetComponent<Text>().text += speaker + " : " + dialogue + "\n\n";
            }
        }
    }
}
