using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoOptionButton : MonoBehaviour
{
    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    private Button Button_1, Button_2;
    private int CurrentIndex;
    private int NextIndex;

    // Start is called before the first frame update
    void Start()
    {
        CurrentIndex = NextIndex = 1;

        Button_1 = this.GetComponentsInChildren<Button>()[1];
        Button_2 = this.GetComponentsInChildren<Button>()[0];

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        Button_1.colors = newColor;
        Button_2.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        Button_1.colors = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (NextIndex == CurrentIndex) return;

        CurrentIndex = NextIndex;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        Button_1.colors = newColor;
        Button_2.colors = newColor;

        switch (CurrentIndex)
        {
            case 1:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                Button_1.colors = newColor;

                break;
            case 2:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                Button_2.colors = newColor;

                break;
        }
    }

    public void SetIndex(int index)
    {
        NextIndex = index;
    }

    public int GetIndex()
    {
        return CurrentIndex;
    }

    private void OnEnable()
    {
        CurrentIndex = NextIndex = 1;

        Button_1 = this.GetComponentsInChildren<Button>()[1];
        Button_2 = this.GetComponentsInChildren<Button>()[0];

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        Button_1.colors = newColor;
        Button_2.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        Button_1.colors = newColor;
    }
}