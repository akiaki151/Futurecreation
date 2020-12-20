using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEffectGen : MonoBehaviour
{
    public GameObject GeneratePoint;
    public GameObject HeartEffect;

    const int maxHeartCount = 20;

    float span = 0.08f;
    float delta = 0.0f;
    int heartCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // なでなで中か？
        if (!HandCtl.isNadeNade) return;

        // 手が動いているか？
        HandCtl hand = GeneratePoint.transform.GetChild(0).GetComponent<HandCtl>();
        if (!hand.IsMovement()) return;


        // 一旦処理を止めます
        if (false)
        {
            // ハートの数が一定数超えたか？
            HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
            if (heartCount > heart.GetMaxHeartCount()) return;
        }


        delta += Time.deltaTime;    // デルタを加算
        if (delta > span)
        {
            delta = 0.0f;   // デルタをリセット

            // 生成
            GameObject effect = Instantiate(HeartEffect) as GameObject;
            effect.transform.position = new Vector3(GeneratePoint.transform.GetChild(0).position.x, GeneratePoint.transform.GetChild(0).position.y, 0.06f);
            effect.transform.Rotate(0.0f, 0.0f, 0.0f);

            // 子オブジェクトにセットする
            effect.transform.parent = this.transform;

            // ハートの数を加算
            heartCount++;
        }

    }


    public int GetHeartCount() { return heartCount; }
    public int GetMaxHeartCount() { return maxHeartCount; }
    public void SetHeartCount(int HeartCount) { heartCount = HeartCount; }
}
