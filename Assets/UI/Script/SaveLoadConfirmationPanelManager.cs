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
    private void Awake()
    {
        CurrentIndex = NextIndex = 0;
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.GetComponent<TwoOptionButton>().SetIndex(1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.GetComponent<TwoOptionButton>().SetIndex(2);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch(this.GetComponent<TwoOptionButton>().GetIndex())
            {
                case 1:
                    switch (CurrentIndex)
                    {
                        case 1:
                            this.GetComponent<TwoOptionButton>().SetIndex(1);
                            SetObjectName();
                            this.gameObject.SetActive(false);
                            break;

                        case 2:
                            this.GetComponent<TwoOptionButton>().SetIndex(2);
                            this.gameObject.SetActive(false);
                            break;
                    }
                    break;
                case 2:
                    this.GetComponent<TwoOptionButton>().SetIndex(2);
                    this.gameObject.SetActive(false);
                    break;
            }
        }

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