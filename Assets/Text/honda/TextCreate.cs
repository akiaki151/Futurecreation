using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class TextCreate : MonoBehaviour
{
    //public string[] sentences; // 文章を格納する
    public Text uiText;   // uiTextへの参照

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalDisplay = 0.05f;   // 1文字の表示にかける時間

    private string[][] m_stock_text = new string[500][];
    private string m_text;
    private int currentSentenceNum = 0; //現在表示している文章番号
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeBeganDisplay = 1;         // 文字列の表示を開始した時間
    private int lastUpdateCharCount = -1;       // 表示中の文字数


    // Start is called before the first frame update
    void Start()
    {
        // ファイル読み込み
        // 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
        StreamReader sr = new StreamReader(@"TextData.csv", Encoding.GetEncoding("Shift_JIS"));
        string line;
        int num = 0;
        // 行がnullじゃない間(つまり次の行がある場合は)、処理をする
        while ((line = sr.ReadLine()) != null)
        {
            string split_text = line;
            m_stock_text[num] = split_text.Split(","[0]);
            // コンソールに出力
            Debug.Log(line);
            num++;
        }
        // StreamReaderを閉じる
        sr.Close();

        SetNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        //表示される文字数を計算
        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * m_text.Length);
        //表示される文字数が表示している文字数と違う
        if (displayCharCount != lastUpdateCharCount)
        {
            uiText.text = m_text.Substring(0, displayCharCount);
            //表示している文字数の更新
            lastUpdateCharCount = displayCharCount;
        }
    }

    // 次の文章をセットする
    void SetNextSentence()
    {
        //uiText.text = m_stock_text[currentSentenceNum][2];
        //currentSentence = m_stock_text[currentSentenceNum][2];
        m_text = m_stock_text[currentSentenceNum][2];
        timeUntilDisplay = m_text.Length * intervalDisplay;
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }

    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        string Tag = collision.gameObject.tag;
        if (Tag == "Mouse")
        {
            // 文章の表示完了 / 未完了
            if (IsDisplayComplete())
            {
                //最後の文章ではない & ボタンが押された
                if (Input.GetMouseButtonDown(0))
                {
                    SetNextSentence();
                }
            }
            //else
            //{
            //    //ボタンが押された
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        timeUntilDisplay = 0;
            //    }
            //}
        }
    }
}
