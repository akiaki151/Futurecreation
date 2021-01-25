using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPanelChangeComment : MonoBehaviour
{
    [SerializeField] GameObject 
        DataPanel1, DataPanel2, DataPanel3, DataPanel4, DataPanel5, DataPanel6, 
        DataPanel7, DataPanel8, DataPanel9, DataPanel10, DataPanel11, DataPanel12;

    [SerializeField] private ColorBlock SelectableColor;
    [SerializeField] private ColorBlock NormalColor;

    private Button Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button10, Button11, Button12;
    private InputField Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12;
    private SaveLoadDataManager DataManager;

    // Start is called before the first frame update
    void Start()
    {
        DataManager = GameObject.Find("SaveLoadWindow").GetComponent<SaveLoadDataManager>();

        Button1 = DataPanel1.GetComponentInChildren<Button>();
        Button2 = DataPanel2.GetComponentInChildren<Button>();
        Button3 = DataPanel3.GetComponentInChildren<Button>();
        Button4 = DataPanel4.GetComponentInChildren<Button>();
        Button5 = DataPanel5.GetComponentInChildren<Button>();
        Button6 = DataPanel6.GetComponentInChildren<Button>();
        Button7 = DataPanel7.GetComponentInChildren<Button>();
        Button8 = DataPanel8.GetComponentInChildren<Button>();
        Button9 = DataPanel9.GetComponentInChildren<Button>();
        Button10 = DataPanel10.GetComponentInChildren<Button>();
        Button11 = DataPanel11.GetComponentInChildren<Button>();
        Button12 = DataPanel12.GetComponentInChildren<Button>();

        Field1 = DataPanel1.GetComponentInChildren<InputField>();
        Field2 = DataPanel2.GetComponentInChildren<InputField>();
        Field3 = DataPanel3.GetComponentInChildren<InputField>();
        Field4 = DataPanel4.GetComponentInChildren<InputField>();
        Field5 = DataPanel5.GetComponentInChildren<InputField>();
        Field6 = DataPanel6.GetComponentInChildren<InputField>();
        Field7 = DataPanel7.GetComponentInChildren<InputField>();
        Field8 = DataPanel8.GetComponentInChildren<InputField>();
        Field9 = DataPanel9.GetComponentInChildren<InputField>();
        Field10 = DataPanel10.GetComponentInChildren<InputField>();
        Field11 = DataPanel11.GetComponentInChildren<InputField>();
        Field12 = DataPanel12.GetComponentInChildren<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataPanel1.gameObject.activeInHierarchy)
        {
            if (DataManager.GetSaveLoadIndex() == 4)
            {
                Button1.gameObject.SetActive(false);
                Button2.gameObject.SetActive(false);
                Button3.gameObject.SetActive(false);
                Button4.gameObject.SetActive(false);
                Button5.gameObject.SetActive(false);
                Button6.gameObject.SetActive(false);
                Button7.gameObject.SetActive(false);
                Button8.gameObject.SetActive(false);
                Button9.gameObject.SetActive(false);
                Button10.gameObject.SetActive(false);
                Button11.gameObject.SetActive(false);
                Button12.gameObject.SetActive(false);

                Field1.colors = SelectableColor;
                Field2.colors = SelectableColor;
                Field3.colors = SelectableColor;
                Field4.colors = SelectableColor;
                Field5.colors = SelectableColor;
                Field6.colors = SelectableColor;
                Field7.colors = SelectableColor;
                Field8.colors = SelectableColor;
                Field9.colors = SelectableColor;
                Field10.colors = SelectableColor;
                Field11.colors = SelectableColor;
                Field12.colors = SelectableColor;
            }
        }

        if (DataManager.GetSaveLoadIndex() != 4)
        {
            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(true);
            Button3.gameObject.SetActive(true);
            Button4.gameObject.SetActive(true);
            Button5.gameObject.SetActive(true);
            Button6.gameObject.SetActive(true);
            Button7.gameObject.SetActive(true);
            Button8.gameObject.SetActive(true);
            Button9.gameObject.SetActive(true);
            Button10.gameObject.SetActive(true);
            Button11.gameObject.SetActive(true);
            Button12.gameObject.SetActive(true);

            Field1.colors = NormalColor;
            Field2.colors = NormalColor;
            Field3.colors = NormalColor;
            Field4.colors = NormalColor;
            Field5.colors = NormalColor;
            Field6.colors = NormalColor;
            Field7.colors = NormalColor;
            Field8.colors = NormalColor;
            Field9.colors = NormalColor;
            Field10.colors = NormalColor;
            Field11.colors = NormalColor;
            Field12.colors = NormalColor;
        }
    }
}
