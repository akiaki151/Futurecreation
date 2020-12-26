using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadPagemanager : MonoBehaviour
{
    [SerializeField] private int CurrentPageNum;
    [SerializeField] private int NextPageNum;
    [SerializeField] private Button Page1;
    [SerializeField] private Button Page2;
    [SerializeField] private Button Page3;

    [SerializeField] private ColorBlock SelectedButtonColor;
    [SerializeField] private ColorBlock UnselectedButtonColor;

    // Start is called before the first frame update
    void Awake()
    {
        CurrentPageNum = NextPageNum = 1;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        Page1.colors = newColor;
        Page2.colors = newColor;
        Page3.colors = newColor;

        newColor = SelectedButtonColor;

        newColor.highlightedColor = SelectedButtonColor.highlightedColor;
        newColor.normalColor = SelectedButtonColor.normalColor;
        newColor.pressedColor = SelectedButtonColor.pressedColor;
        newColor.selectedColor = SelectedButtonColor.selectedColor;
        newColor.disabledColor = SelectedButtonColor.disabledColor;
        Page1.colors = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPageNum == NextPageNum) return;
        CurrentPageNum = NextPageNum;

        ColorBlock newColor = UnselectedButtonColor;

        newColor.highlightedColor = UnselectedButtonColor.highlightedColor;
        newColor.normalColor = UnselectedButtonColor.normalColor;
        newColor.pressedColor = UnselectedButtonColor.pressedColor;
        newColor.selectedColor = UnselectedButtonColor.selectedColor;
        newColor.disabledColor = UnselectedButtonColor.disabledColor;
        Page1.colors = newColor;
        Page2.colors = newColor;
        Page3.colors = newColor;

        switch (CurrentPageNum)
        {
            case 1:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                Page1.colors = newColor;
                break;
            case 2:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                Page2.colors = newColor;
                break;
            case 3:
                newColor = SelectedButtonColor;

                newColor.highlightedColor = SelectedButtonColor.highlightedColor;
                newColor.normalColor = SelectedButtonColor.normalColor;
                newColor.pressedColor = SelectedButtonColor.pressedColor;
                newColor.selectedColor = SelectedButtonColor.selectedColor;
                newColor.disabledColor = SelectedButtonColor.disabledColor;
                Page3.colors = newColor;
                break;
        }
    }

    public void ChangePage(int i)
    {
        NextPageNum = i;
    }

    public int GetCurrentPage()
    {
        return CurrentPageNum;
    }
}
