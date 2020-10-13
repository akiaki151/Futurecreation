using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = this.gameObject.transform.Find("Slider").GetComponent<Slider>().value;

        this.gameObject.transform.Find("Value").GetComponent<Text>().text = value.ToString();
    }
}
