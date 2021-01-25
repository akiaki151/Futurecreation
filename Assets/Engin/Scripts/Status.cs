using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Status : MonoBehaviour
{
    private float x_pos, y_pos, out_scale;
    void Start()
    {
        GameObject.Find("X_InputPos").GetComponent<InputField>().text = "0.0";
        GameObject.Find("Y_InputPos").GetComponent<InputField>().text = "-50.0";
        GameObject.Find("InputScale").GetComponent<InputField>().text = "1.0";
    }

    public void Set_Pos()
    {
        float.TryParse(GameObject.Find("X_InputPos").GetComponent<InputField>().text, out x_pos);
        float.TryParse(GameObject.Find("Y_InputPos").GetComponent<InputField>().text, out y_pos);
        GameObject.Find("CharacterButton").GetComponent<RectTransform>().localPosition = new Vector3(x_pos, y_pos, -1);
        Debug.Log(GameObject.Find("CharacterButton").GetComponent<RectTransform>().localPosition);
    }
    public void SetScale()
    {
        float.TryParse(GameObject.Find("InputScale").GetComponent<InputField>().text, out out_scale);
        if (out_scale != 0)
        {
            GameObject.Find("CharacterButton").GetComponent<RectTransform>().localScale = new Vector3(out_scale, out_scale, 1);
        }
    }
}
