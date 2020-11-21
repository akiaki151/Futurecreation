using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoutePoint
{
    // 移動用のTransform
    public Transform StartPoint;    // 始点
    public Transform EndPoint;      // 終点
    public Transform CtlPoint;      // 制御点

    /* ↓必要なものがあったら随時追加↓ */
}