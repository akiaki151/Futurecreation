using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] Button Button_1, Button_2;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        Vector3 temp = Button_1.transform.localPosition;
        temp.y = this.transform.localPosition.y;
        Button_1.transform.localPosition = temp;
        temp = Button_2.transform.localPosition;
        temp.y = this.transform.localPosition.y;
        Button_2.transform.localPosition = temp;
        Button_1.gameObject.SetActive(true);
        Button_2.gameObject.SetActive(true);
    }
}
