using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NadeNadeCtl : MonoBehaviour
{
    [SerializeField]
    HandCtl hand = null;

    [SerializeField]
    private List<RoutePoint> routes = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // まだ仮
        for (int i = 0; i < routes.Count; i++)
        {
            Vector3 c1 = routes[i].Point.position;
            float radius1 = 0.25f;

            Vector3 c2 = hand.transform.position;
            float radius2 = 0.5f;

            float distSq = (c1.x - c2.x) * (c1.x - c2.x) + (c1.y - c2.y) * (c1.y - c2.y);

            if (distSq <= (radius1 + radius2) * (radius1 + radius2))
            {
                Debug.Log("当たった");
                HandCtl.isNadeNade = true;
            }
            else
            {
                Debug.Log("当たってない");
            }
        }
    }

    public Vector3 GetRoutesPoint(int index) { return routes[index].Point.position; }
}