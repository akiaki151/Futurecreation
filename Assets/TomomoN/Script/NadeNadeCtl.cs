using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NadeNadeCtl : MonoBehaviour
{
    [SerializeField]
    private List<RoutePoint> routes = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetRoutesPoint(int index) { return routes[index].Point.position; }
}