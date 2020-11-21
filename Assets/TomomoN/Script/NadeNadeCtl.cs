using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NadeNadeCtl : MonoBehaviour
{
    [SerializeField]
    private List<RoutePoint> routes = null;

    

    public Vector3 GetStartPoint(int index) { return routes[index].StartPoint.position; }
    public Vector3 GetEndPoint(int index) { return routes[index].EndPoint.position; }
    public Vector3 GetCtlPoint(int index) { return routes[index].CtlPoint.position; }
}