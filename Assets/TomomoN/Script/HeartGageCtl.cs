using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartGageCtl : MonoBehaviour
{
    //　ロード中に表示するUI画面
    [SerializeField] private GameObject loadUI = null;

    //　読み込み率を表示するスライダー
    [SerializeField] private GameObject HeartEffect = null;

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
        HeartEffectGen heart = HeartEffect.GetComponent<HeartEffectGen>();
        float heartNum = heart.GetHeartCount();
        float heartMax = heart.GetMaxHeartCount();

        slider.value = heartNum / heartMax;
    }
}
