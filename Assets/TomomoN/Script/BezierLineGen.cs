using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierLineGen : MonoBehaviour
{
    GameObject BezierLine = null;

    // Start is called before the first frame update
    void Start()
    {
        BezierLine = this.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // なでなで中か？
        if (!HandCtl.isNadeNade)
        {
            BezierLine.SetActive(true);
        }
        else
        {
            BezierLine.SetActive(false);
        }
    }
}
