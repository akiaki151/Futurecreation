using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierLineRenderer : MonoBehaviour
{
    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    private Transform endPosition;
    [SerializeField]
    private Transform controlPoint;
    [SerializeField]
    private int middlePoints = 10;

    [SerializeField]
    private bool considerDistance = true;
    [SerializeField]
    private float distanceEffect = 0.2f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer render = this.GetComponent<LineRenderer>();
        //render.startWidth = 0.2f;
        //render.endWidth = 0.2f;

        float de = 1.0f;
        if (considerDistance)
        {
            de = (startPosition.position - endPosition.position).magnitude * distanceEffect;
        }
        Vector3 control = (startPosition.position + endPosition.position) / 2 + controlPoint.position * de;

        int totalPoints = middlePoints + 2;
        render.positionCount = totalPoints;

        render.SetPosition(0, new Vector3(startPosition.position.x, startPosition.position.y, 0.0f));
        for (int i = 1; i <= middlePoints; i++)
        {
            float t = (float)i / (float)(totalPoints - 1);
            Vector3 mpos = BezierCurve(startPosition.position, endPosition.position, control, t);
            render.SetPosition(i, new Vector3(mpos.x, mpos.y, 0.0f));
        }
        render.SetPosition(totalPoints - 1, new Vector3(endPosition.position.x, endPosition.position.y, 0.0f));
    }



    Vector3 BezierCurve(Vector3 startPos, Vector3 endPos, Vector3 controlPos, float t)
    {
        Vector3 p0 = Vector3.Lerp(startPos, controlPos, t); // ラインp0に沿って補間
        Vector3 p1 = Vector3.Lerp(controlPos, endPos, t);   // ラインp1に沿って補間
        
        Vector3 p2 = Vector3.Lerp(p0, p1, t);               // ラインp2に沿って補間

        return p2;
    }
}
