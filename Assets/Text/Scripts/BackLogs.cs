using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackLogs : MonoBehaviour
{
    public void DialogueText(string text)
    {
        GameObject.Find("LogText").GetComponent<Text>().text = text;
    }
}
