using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadScreenScroll : MonoBehaviour
{
    [SerializeField] GameObject DataPanel;
    [SerializeField] Slider slider;

    private int OldSliderValue;
    private int SliderValue;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { SliderTaskOnChanged(); });
        OldSliderValue = SliderValue = (int)slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (SliderValue != OldSliderValue)
        {
            Vector3 temp = DataPanel.transform.position;

            temp.x += (OldSliderValue - SliderValue) * 1.584f;

            DataPanel.transform.position = temp;

            OldSliderValue = SliderValue;
        }
    }

    public void SliderTaskOnChanged()
    {
        SliderValue = (int)slider.value;
    }
}
