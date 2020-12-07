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

        ValueSlider.value = 50;

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
    }

    public void SliderTaskOnChanged()
    {
        ValueInputField.text = ValueSlider.value.ToString();
    }
}