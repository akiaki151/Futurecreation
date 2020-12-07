using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TextMP : MonoBehaviour
{
    private TextMeshPro text;
    public bool vertical;        //縦書きか
    private Vector3[] vec;
    private bool once;
    private int cnt;
    public int i;
    private TMPAnimation action;

    // Start is called before the first frame update
    void Start()
    {
       text = GetComponent<TextMeshPro>();



        // Quaternion rot = text.rectTransform.rotation;
        // Vector3 pos = text.rectTransform.position;
        // pos.y -= 3.0f;
        // rot.z = -1;
        // rot.x = 0;
        // rot.y = 0;
        // text.rectTransform.rotation = rot;
        // text.rectTransform.position = pos;
        // once = true;
        // cnt = 0;

        // action =new TMPAnimation();
        action = GetComponent<TMPAnimation>();


        action.i = i;
        action.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < text.textInfo.characterCount; i++)
        //{
        //    var charaInfo = text.textInfo.characterInfo[i];

        //    if (!charaInfo.isVisible)
        //    {
        //        continue;
        //    }

        //    int materialIndex = charaInfo.materialReferenceIndex;
        //    int vertexIndex = charaInfo.vertexIndex;

        //    Vector3[] destVertices = text.textInfo.meshInfo[materialIndex].vertices;

        //    //float angle = 180 * Mathf.Sin(Time.time / 100);

        //    Vector3 rotateCenterVertex = Vector3.Lerp(destVertices[vertexIndex + 1], destVertices[vertexIndex + 2], 0.5f);

        //    destVertices[vertexIndex + 0] += -rotateCenterVertex;
        //    destVertices[vertexIndex + 1] += -rotateCenterVertex;
        //    destVertices[vertexIndex + 2] += -rotateCenterVertex;
        //    destVertices[vertexIndex + 3] += -rotateCenterVertex;

        //    Matrix4x4 matrix = new Matrix4x4();
        //    matrix.SetTRS(Vector3.zero, Quaternion.Euler(0, 0, 90), Vector3.one);
        //    matrix = Matrix4x4.identity;

        //    matrix.m00 = 0;
        //    matrix.m01 = -1;
        //    matrix.m10 = 1;
        //    matrix.m11 = 0;
        //    matrix.m22 = 1;
        //    matrix.m33 = 1;

        //    destVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 0]);
        //    destVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 1]);
        //    destVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 2]);
        //    destVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 3]);

        //    destVertices[vertexIndex + 0] += rotateCenterVertex;
        //    destVertices[vertexIndex + 1] += rotateCenterVertex;
        //    destVertices[vertexIndex + 2] += rotateCenterVertex;
        //    destVertices[vertexIndex + 3] += rotateCenterVertex;


        //}

        //for (int i = 0; i < text.textInfo.meshInfo.Length; i++)
        //{
        //    text.textInfo.meshInfo[i].mesh.vertices = text.textInfo.meshInfo[i].vertices;
        //    text.UpdateGeometry(text.textInfo.meshInfo[i].mesh, i);
        //}

        //action.BundAnimation(text);
        action.SetAnim();
    }

}
