using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Imageselect : MonoBehaviour
{
    public void IconImageView()
    {
        GameObject button = GameObject.Find("iconButton");
        button.GetComponent<Button>().image.sprite = gameObject.GetComponent<Button>().image.sprite;
    }
    public void CharacterImageView()
    {
        GameObject button = GameObject.Find("CharacterButton");
        button.GetComponent<Button>().image.sprite = gameObject.GetComponent<Button>().image.sprite;
    }
    public void BackImageView()
    {
        GameObject img = GameObject.Find("BackImage");
        img.GetComponent<Image>().sprite = gameObject.GetComponent<Button>().image.sprite;
    }
    public void SESelect()
    {
        GameObject SE = GameObject.Find("SENameFile");
        if (!gameObject.GetComponent<Button>().GetComponentInChildren<Toggle>().isOn)
            SE.GetComponentInChildren<Text>().text = "SE : " + gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text + "(0)";
        else
            SE.GetComponentInChildren<Text>().text = "SE : " + gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text + "(1)";
    }
    public void BGMSelect()
    {
        GameObject BGM = GameObject.Find("BGMNameFile");
        if(!gameObject.GetComponent<Button>().GetComponentInChildren<Toggle>().isOn)
            BGM.GetComponentInChildren<Text>().text = "BGM : " + gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text + "(0)";
        else
            BGM.GetComponentInChildren<Text>().text = "BGM : " + gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text + "(1)";
    }
}
