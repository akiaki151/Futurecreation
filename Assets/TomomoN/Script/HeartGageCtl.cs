using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartGageCtl : MonoBehaviour
{
    //　ロード中に表示するUI画面
    [SerializeField] private GameObject loadUI = null;

    //　ハートエフェクト（成功）
    [SerializeField] private GameObject HeartGenerator = null;

    //　読み込み率を表示するスライダー
    [SerializeField] private Slider slider = null;

    // Start is called before the first frame update
    void Start()
    {
        loadUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HeartEffectGen heart = HeartGenerator.GetComponent<HeartEffectGen>();
        float heartNum = heart.GetHeartCount();
        float heartMax = heart.GetMaxHeartCount();

        slider.value = heartNum / heartMax;
    }
}
//public class HeartGageCtl : MonoBehaviour
//{
//    //　ロード中に表示するUI画面
//    [SerializeField] private GameObject loadUI = null;

//    //　ハートエフェクト（成功）
//    [SerializeField] private GameObject HeartGenerator = null;

//    //　ハートエフェクト（部分成功）
//    [SerializeField] private GameObject PartHeartEffect = null;

//    //　読み込み率を表示するスライダー
//    [SerializeField] private Slider slider = null;

//    // Start is called before the first frame update
//    void Start()
//    {
//        loadUI.SetActive(true);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        GifTextureScript gif = GameObject.Find("GifImage").GetComponent<GifTextureScript>();
//        if (!gif.IsGifPlaying())
//        {
//            HeartEffectGen heart = HeartGenerator.GetComponent<HeartEffectGen>();
//            float heartNum = heart.GetHeartCount();
//            float heartMax = heart.GetMaxHeartCount();

//            slider.value = heartNum / heartMax;
//        }


//        Debug.Log(slider.value);

//        // 一定率でエフェクト表示
//        if (slider.value >= 0.0f)
//        {
//            // 生成
//            GameObject effect = Instantiate(PartHeartEffect) as GameObject;
//            effect.transform.position = new Vector3(-8.0f, 4.0f, 0.06f);
//            effect.transform.Rotate(0.0f, 0.0f, 0.0f);

//            // 子オブジェクトにセットする
//            effect.transform.parent = this.transform;

//            slider.value = 0.0f;
//            Debug.Log(slider.value);
//        }
//    }
//}
