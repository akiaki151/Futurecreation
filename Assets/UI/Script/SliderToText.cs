using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    private InputField ValueInputField;
    private Slider ValueSlider;

    // Start is called before the first frame update
    void Start()
    {
        ValueInputField = this.gameObject.transform.Find("Value").GetComponent<InputField>();
        ValueSlider = this.gameObject.transform.Find("Slider").GetComponent<Slider>();

        ValueInputField.onValueChanged.AddListener(delegate { InputFieldTaskOnChanged(); });

        ValueSlider.onValueChanged.AddListener(delegate { SliderTaskOnChanged(); });

        if (PlayerPrefs.GetInt(this.name) == 0)
        {
            ValueSlider.value = 50;
            PlayerPrefs.SetInt(this.name, (int)ValueSlider.value);
        }
        else
        {
            ValueSlider.value = PlayerPrefs.GetInt(this.name);
        }

        ValueInputField.text = ValueSlider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputFieldTaskOnChanged()
    {
        if (int.Parse(ValueInputField.text) > 100)
        {
            ValueInputField.text = "100";
        }
        if (int.Parse(ValueInputField.text) < 0)
        {
            ValueInputField.text = "0";
        }

        ValueSlider.value = int.Parse(ValueInputField.text);
        PlayerPrefs.SetInt(this.name, (int)ValueSlider.value);
    }

    public void SliderTaskOnChanged()
    {
        ValueInputField.text = ValueSlider.value.ToString();
        PlayerPrefs.SetInt(this.name, (int)ValueSlider.value);
    }

    public void AddValue(int value)
    {
        ValueSlider.value += value;
    }

    public void SubsValue(int value)
    {
        ValueSlider.value -= value;
    }

    public float GetValue()
    {
        return ValueSlider.value;
    }
}