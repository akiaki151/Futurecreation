using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenDataPanel : MonoBehaviour
{
    [SerializeField] GameObject DataPanel;

    [SerializeField] Slider slider;

    [SerializeField] private Sprite SaveSprite_1;
    [SerializeField] private Sprite SaveSprite_2;
    [SerializeField] private Sprite LoadSprite_1;
    [SerializeField] private Sprite LoadSprite_2;

    [SerializeField] Button SaveButton;
    [SerializeField] Button LoadButton;

    private enum DataPanelIndex
    {
        NONE = 0,
        SAVE,
        LOAD
    }

    private int CurrentDataPanelIndex;
    private int NextDataPanelIndex;

    private int OldSliderValue;
    private int SliderValue;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { SliderTaskOnChanged(); });
        OldSliderValue = SliderValue = (int)slider.value;

        CurrentDataPanelIndex = 0;
        NextDataPanelIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (SliderValue != OldSliderValue)
        {
            Vector3 temp = DataPanel.transform.position;

            temp.y -= (OldSliderValue - SliderValue) * 0.05f;

            DataPanel.transform.position = temp;

            OldSliderValue = SliderValue;
        }

        if (NextDataPanelIndex == CurrentDataPanelIndex) return;

        CurrentDataPanelIndex = NextDataPanelIndex;

        switch (CurrentDataPanelIndex)
        {
            case 1:
                SaveButton.image.sprite = SaveSprite_2;
                LoadButton.image.sprite = LoadSprite_1;
                break;
            case 2:
                SaveButton.image.sprite = SaveSprite_1;
                LoadButton.image.sprite = LoadSprite_2;
                break;
        }
    }

    public void SliderTaskOnChanged()
    {
        SliderValue = (int)slider.value;
    }

    public void SetDataPanelIndex(int index)
    {
        NextDataPanelIndex = index;
    }

    public int GetSettingIndex()
    {
        return CurrentDataPanelIndex;
    }
}
