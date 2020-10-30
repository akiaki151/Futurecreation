using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Imageselect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
