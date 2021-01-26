using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoButtonScript : MonoBehaviour
{
    private bool IsAuto;
    private Button button;

    [SerializeField] private ColorBlock IsNotAutoButtonColor;
    [SerializeField] private ColorBlock IsAutoButtonColor;

    private void Awake()
    {
        IsAuto = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        ColorBlock newColor = IsNotAutoButtonColor;
        PlayerPrefs.SetInt("auto", 0);
        newColor.highlightedColor = IsNotAutoButtonColor.highlightedColor;
        newColor.normalColor = IsNotAutoButtonColor.normalColor;
        newColor.pressedColor = IsNotAutoButtonColor.pressedColor;
        newColor.selectedColor = IsNotAutoButtonColor.selectedColor;
        newColor.disabledColor = IsNotAutoButtonColor.disabledColor;
        this.GetComponent<Button>().colors = IsNotAutoButtonColor;

        newColor = IsNotAutoButtonColor;

        newColor.highlightedColor = IsAutoButtonColor.highlightedColor;
        newColor.normalColor = IsAutoButtonColor.normalColor;
        newColor.pressedColor = IsAutoButtonColor.pressedColor;
        newColor.selectedColor = IsAutoButtonColor.selectedColor;
        newColor.disabledColor = IsAutoButtonColor.disabledColor;

        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAuto(bool value)
    {
        IsAuto = value;

        if (value)
        {
            this.GetComponent<Button>().colors = IsAutoButtonColor;
            PlayerPrefs.SetInt("auto", 1);
        }
        else
        {
            this.GetComponent<Button>().colors = IsNotAutoButtonColor;
            PlayerPrefs.SetInt("auto", 0);
        }
    }

    public bool GetAuto()
    {
        return IsAuto;
    }

    void TaskOnClick()
    {
        if (IsAuto)
        {
            SetAuto(false);
        }
        else
        {
            SetAuto(true);
        }
    }
}
