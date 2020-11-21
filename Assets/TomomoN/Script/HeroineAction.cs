using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroineAction : MonoBehaviour
{
    const int maxActionCount = 5;   // フリフリの回数

    int actionCount = 0;
    float speed = 17.0f;
    bool bHurihuri = false;
    bool bAction = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // ハートの数が一定数超えない場合returnする
        HeartEffectGen heart = GameObject.Find("HeartEffectGenerator").GetComponent<HeartEffectGen>();
        if (heart.GetHeartCount() < heart.GetMaxHeartCount()) return;


        float step = speed * Time.deltaTime;    // 回転する速さ(speedの値を変える)

        // maxActionCountまでフリフリしたら
        if (actionCount > maxActionCount)       
        {
            // 元の位置に戻す
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), step);
            if (this.transform.rotation.z * Mathf.Rad2Deg > -0.25f)
            {
                // 全ての値を戻す
                bAction = false;
                bHurihuri = false;
                actionCount = 0;
                heart.SetHeartCount(0);
                this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
        }
        else
        {
            bAction = true;
            if (!bHurihuri)   // 左にフリフリ
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 5.0f), step);
                if (this.transform.rotation.z * Mathf.Rad2Deg > 2.4f)
                {
                    bHurihuri = true;
                    actionCount++;
                }
            }
            else   // 右にフリフリ
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0.0f, 0.0f, -5.0f), step);
                if (this.transform.rotation.z * Mathf.Rad2Deg < -2.4f)
                {
                    bHurihuri = false;
                    actionCount++;
                }
            }
        }

    }



    public bool IsAction() { return bAction; }
}
