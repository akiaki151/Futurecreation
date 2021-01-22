using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadConfirmationPanelManager : MonoBehaviour
{
    [SerializeField] Sprite SaveImage, LoadImage;

    private enum ConfirmationIndex
    {
        NONE = 0,
        SAVE,
        LOAD,
    }

    [SerializeField] private int CurrentIndex;
    [SerializeField] private int NextIndex;

    private GameObject Gameobject;
    private Save save;

    // Start is called before the first frame update
    void Start()
    {
        CurrentIndex = NextIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (NextIndex == CurrentIndex) return;

        CurrentIndex = NextIndex;

        switch (CurrentIndex)
        {
            case 1:
                transform.Find("Image").GetComponent<Image>().sprite = SaveImage;
                break;
            case 2:
                transform.Find("Image").GetComponent<Image>().sprite = LoadImage;
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

    public void GetObjectName(GameObject obj)
    {
        Gameobject = obj;
    }

    public void SetObjectName()
    {
        save = Gameobject.GetComponent<Save>();
        save.OnClickSaveLoad();
    }
}