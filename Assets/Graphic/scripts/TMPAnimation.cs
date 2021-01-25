using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.UI;

public class TMPAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AnimationCurve scaleAnimationX;
    [SerializeField]
    private AnimationCurve scaleAnimationY;
    [SerializeField]
    private AnimationCurve rotateAnimation;

    // Start is called before the first frame update
    private TMP_MeshInfo[] cachedMeshInfo;
    private Vector3[] sourceVertices;
    private Vector3[] verts;
    private float x = 0.0f;
    private float anim;
    private float startTime;
    private Matrix4x4 matrix;
    private Vector3 centerVec;
    private Vector3 scale;
    private Vector3 rot;
    public float speed; 
    public int frame;
    public int velocity;
    public int delay;
    private GameObject _canvas;
    private Vector3 _save_pos;
    private float nowDegree;

    public TextMeshProUGUI text;

    private string[] funcName = { "BundAnimation", "UpAnimation", "RotateAnimation" };

    public int i;

    public void SetAnim()
    {
        //Debug.Log("動いた");
        //speed = time;
        Invoke(funcName[i], 0);

    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        

    }
    void Start()
    {
        Image obj;
        
        _canvas = GameObject.Find("Canvas");
        foreach (Transform child in _canvas.transform)
        {
            if (child.name == "MenuBar")
            {
               foreach(Transform child2 in child.transform)
                {
                    if(child2.name== "SaveButton")
                    {
                         obj = child2.GetComponent<Image>();
                        _save_pos = obj.rectTransform.localPosition;
                        Debug.Log(_save_pos);
                    }
                }
            }
        }
        speed = 0.0f;

        // _save_pos.y -= 10;

        text.rectTransform.localPosition = new Vector3(0.0f, -65.0f, 0.0f);
        

      //  Invoke(funcName[i], 0);
    }

    private void Update()
    {
        //BundAnimation();
    }

    public void BundAnimation()
    {
        
        //speed+=0.01f;

        startTime = Time.time;
        text.ForceMeshUpdate();

        if (text.textInfo.characterCount == 0)
        {
            return;
        }

        cachedMeshInfo = text.textInfo.CopyMeshInfoVertexData();

        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            var charInfo = text.textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            sourceVertices = cachedMeshInfo[materialIndex].vertices;
            verts = text.textInfo.meshInfo[materialIndex].vertices;
            centerVec = (sourceVertices[vertexIndex + 1] + sourceVertices[vertexIndex + 2]) / 2;

            x = ((velocity * speed) - (i * delay * 9.8f / (speed + 0.001f) * (speed + 0.0001f)) / 2) / frame;

            anim = curve.Evaluate(x) * 3.0f;
            scale.x = scaleAnimationX.Evaluate(x);
            scale.y = scaleAnimationY.Evaluate(x);
            scale.z = 1.0f;
            rot.z = rotateAnimation.Evaluate(x);

            verts[vertexIndex + 0] += -centerVec;
            verts[vertexIndex + 1] += -centerVec;
            verts[vertexIndex + 2] += -centerVec;
            verts[vertexIndex + 3] += -centerVec;

            matrix = Matrix4x4.TRS(Vector3.up * anim, Quaternion.Euler(0, 0, rot.z), scale);

            verts[vertexIndex + 0] = matrix.MultiplyPoint3x4(verts[vertexIndex + 0]);
            verts[vertexIndex + 1] = matrix.MultiplyPoint3x4(verts[vertexIndex + 1]);
            verts[vertexIndex + 2] = matrix.MultiplyPoint3x4(verts[vertexIndex + 2]);
            verts[vertexIndex + 3] = matrix.MultiplyPoint3x4(verts[vertexIndex + 3]);

            verts[vertexIndex + 0] += centerVec;
            verts[vertexIndex + 1] += centerVec;
            verts[vertexIndex + 2] += centerVec;
            verts[vertexIndex + 3] += centerVec;

        }

        for (int i = 0; i < text.textInfo.materialCount; i++)
        {
            if (text.textInfo.meshInfo[i].mesh == null)
            {
                continue;
            }
            text.textInfo.meshInfo[i].mesh.vertices = text.textInfo.meshInfo[i].vertices;
            text.UpdateGeometry(text.textInfo.meshInfo[i].mesh, i);

        }

    }

    public void UpAnimation()
    {
       // Debug.Log("アップよバレた");

        startTime = Time.time;


        //Debug.Log(curve);

        text.ForceMeshUpdate();

        if (text.textInfo.characterCount == 0)
        {
            return;
        }

        cachedMeshInfo = text.textInfo.CopyMeshInfoVertexData();

        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            var charInfo = text.textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            sourceVertices = cachedMeshInfo[materialIndex].vertices;
            verts = text.textInfo.meshInfo[materialIndex].vertices;
            centerVec = (sourceVertices[vertexIndex + 1] + sourceVertices[vertexIndex + 2]) / 2;

            x = ((velocity * speed) - (i * delay * 9.8f / (speed + 0.001f) * (speed + 0.0001f)) / 2) / frame;

            anim = curve.Evaluate(x) * 3.0f;
            scale.x = scaleAnimationX.Evaluate(x);
            scale.y = scaleAnimationY.Evaluate(x);
            scale.z = 1.0f;

            if (i % 2 == 0)
                rot.z = rotateAnimation.Evaluate(x);
            else
                rot.z = -rotateAnimation.Evaluate(x);

            verts[vertexIndex + 0] += -centerVec;
            verts[vertexIndex + 1] += -centerVec;
            verts[vertexIndex + 2] += -centerVec;
            verts[vertexIndex + 3] += -centerVec;

            matrix = Matrix4x4.TRS(Vector3.up * anim, Quaternion.Euler(0, 0, rot.z), scale);

            verts[vertexIndex + 0] = matrix.MultiplyPoint3x4(verts[vertexIndex + 0]);
            verts[vertexIndex + 1] = matrix.MultiplyPoint3x4(verts[vertexIndex + 1]);
            verts[vertexIndex + 2] = matrix.MultiplyPoint3x4(verts[vertexIndex + 2]);
            verts[vertexIndex + 3] = matrix.MultiplyPoint3x4(verts[vertexIndex + 3]);

            verts[vertexIndex + 0] += centerVec;
            verts[vertexIndex + 1] += centerVec;
            verts[vertexIndex + 2] += centerVec;
            verts[vertexIndex + 3] += centerVec;

        }

        for (int i = 0; i < text.textInfo.materialCount; i++)
        {
            if (text.textInfo.meshInfo[i].mesh == null)
            {
                continue;
            }
            text.textInfo.meshInfo[i].mesh.vertices = text.textInfo.meshInfo[i].vertices;
            text.UpdateGeometry(text.textInfo.meshInfo[i].mesh, i);

        }
    }

    public void RotateAnimation()
    {

        nowDegree = Mathf.Repeat(nowDegree + 1.0f, 360);
        startTime = Time.time;


        //Debug.Log(curve);

        text.ForceMeshUpdate();

        if (text.textInfo.characterCount == 0)
        {
            return;
        }

        cachedMeshInfo = text.textInfo.CopyMeshInfoVertexData();
        float degreeByCharactor = 360f / text.textInfo.characterCount;

        float nowCharactorDegree = nowDegree;

        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            var charInfo = text.textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            verts = text.textInfo.meshInfo[materialIndex].vertices;

            Vector3 zeroDegreePoint = Vector3.up * 40.0f;
            Vector3 moveVector = zeroDegreePoint - 0.5f * (verts[vertexIndex + 2] + verts[vertexIndex + 0]);

            verts[vertexIndex + 0] += moveVector;
            verts[vertexIndex + 1] += moveVector;
            verts[vertexIndex + 2] += moveVector;
            verts[vertexIndex + 3] += moveVector;

            matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, nowCharactorDegree));
            verts[vertexIndex + 0] = matrix.MultiplyPoint3x4(verts[vertexIndex + 0]);
            verts[vertexIndex + 1] = matrix.MultiplyPoint3x4(verts[vertexIndex + 1]);
            verts[vertexIndex + 2] = matrix.MultiplyPoint3x4(verts[vertexIndex + 2]);
            verts[vertexIndex + 3] = matrix.MultiplyPoint3x4(verts[vertexIndex + 3]);

            nowCharactorDegree -= degreeByCharactor;
        }


        for (int i = 0; i < text.textInfo.materialCount; i++)
        {
            if (text.textInfo.meshInfo[i].mesh == null)
            {
                continue;
            }
            text.textInfo.meshInfo[i].mesh.vertices = text.textInfo.meshInfo[i].vertices;
            text.UpdateGeometry(text.textInfo.meshInfo[i].mesh, i);

        }
    }


}
