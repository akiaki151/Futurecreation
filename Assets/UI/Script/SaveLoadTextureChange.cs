using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadTextureChange : MonoBehaviour
{
    [SerializeField] private Sprite SaveImage, LoadImage, QLoadImage;
    [SerializeField] private int CurrentIndex;

    private SaveLoadDataManager DataManager;

    // Start is called before the first frame update
    void Start()
    {
        CurrentIndex = 1;
        DataManager = GameObject.Find("SaveLoadWindow").GetComponent<SaveLoadDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentIndex == DataManager.GetSaveLoadIndex()) return;

        CurrentIndex = DataManager.GetSaveLoadIndex();

        switch(CurrentIndex)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = SaveImage;
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = LoadImage;
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = QLoadImage;
                break;
        }
    }
}
