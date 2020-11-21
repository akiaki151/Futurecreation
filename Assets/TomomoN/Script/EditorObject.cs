using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorObject : MonoBehaviour
{
    public bool bDraw = true;          // ギズモの描画フラグ
    public float radius = 0.2f;        // 大きさ
    public Color color = Color.blue;   // 色

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (!bDraw) return;
        
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}