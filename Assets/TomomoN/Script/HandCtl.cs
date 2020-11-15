using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCtl : MonoBehaviour
{
    public static bool isNadeNade = false;

    private string NadeNadePointName;

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
                // なでなで処理
                Vector3 handPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject nadenade = GameObject.Find("OnePicture");
                Vector3 p1, p2;

                if (NadeNadePointName == "NadeNadeRange")
                {
                    p1 = nadenade.GetComponent<NadeNadeCtl>().GetRoutesPoint(0);
                    p2 = nadenade.GetComponent<NadeNadeCtl>().GetRoutesPoint(1);
                }
                else if(NadeNadePointName == "NadeNadeRange (1)")
                {
                    p1 = nadenade.GetComponent<NadeNadeCtl>().GetRoutesPoint(2);
                    p2 = nadenade.GetComponent<NadeNadeCtl>().GetRoutesPoint(3);
                }
                else
                {
                    p1 = nadenade.GetComponent<NadeNadeCtl>().GetRoutesPoint(4);
                    p2 = nadenade.GetComponent<NadeNadeCtl>().GetRoutesPoint(5);
                }

                float deg = (p2.y - p1.y) / (p2.x - p1.x);
                float seg = p2.y - (deg * p2.x);

                if(-1.5f < deg && deg < 1.5f)
                {
                    // 横長
                    float handPosY = deg * handPosition.x + seg;
                    float handPosX = (handPosY - seg) / deg;


                    handPosition = new Vector3(handPosition.x, handPosY, 0.0f);
                }
                else
                {
                    // 縦長
                    float handPosY = deg * handPosition.x + seg;
                    float handPosX = (handPosition.y - seg) / deg;


                    handPosition = new Vector3(handPosX, handPosition.y, 0.0f);
                }

                this.transform.position = handPosition;


                /* ↓ ---ここに撫でられた時のアクションを書く予定--- ↓ */



                /* ↑ ---------------------------------------------- ↑ */
            }
        }
    }

    
    void OnTriggerStay2D(Collider2D t)
    {
        HandCtl.isNadeNade = true;
        NadeNadePointName = t.gameObject.name;
        Debug.Log("撫でてます");
    }
    void OnTriggerExit2D(Collider2D t)
    {
        HandCtl.isNadeNade = false;
        Debug.Log("はずれた");
    }
}