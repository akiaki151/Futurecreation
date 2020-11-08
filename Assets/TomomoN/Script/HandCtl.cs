using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCtl : MonoBehaviour
{
    public static bool isNadeNade = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))    // 仮（条件によって変更）
        {
            if (!isNadeNade)
            {
                // 移動
                Vector3 handPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Z座標を0にする
                handPosition = new Vector3(handPosition.x, handPosition.y, 0.0f);

                this.transform.position = handPosition;
            }
            else
            {
                // なでなでの移動処理
            }
        }
    }
}