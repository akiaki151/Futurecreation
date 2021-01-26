using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillEffect : MonoBehaviour
{
    Image FillImage = null;
    Color BaseColor;
    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        FillImage = this.GetComponent<Image>();
        BaseColor = FillImage.color;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // なでなで中か？
        if (!HandCtl.isNadeNade)
        {
            FillImage.color = ColorFlash(1.0f);
        }
        else
        {
            HandCtl hand = GameObject.Find("HandPrefab").transform.GetChild(0).GetComponent<HandCtl>();
            if (!hand.IsMovement())
            {
                FillImage.color = ColorFlash(1.0f);
            }
            else
            {
                FillImage.color = ColorFlash(10.0f);
            }
        }
    }


    Color ColorFlash(float speed)
    {
        Color color;
        time += Time.deltaTime * 5.0f * speed;
        color = Vector4.Lerp(new Vector4(BaseColor.r, BaseColor.g, BaseColor.b, BaseColor.a), new Vector4(1.0f, 1.0f, 1.0f, 1.0f), (Mathf.Sin(time) + 1.0f) * 0.5f);

        return color;
    }
}
