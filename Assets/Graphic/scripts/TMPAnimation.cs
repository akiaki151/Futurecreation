using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;


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
    private float x=0.0f;
    private float anim;
    private  float startTime;
    private Matrix4x4 matrix;
    private Vector3 centerVec;
    private Vector3 scale;
    private Vector3 rot;
    public int frame;
    public int velocity;
    public int delay;

    private float nowDegree;

    public TextMeshPro text { private get; set; }
    private string[] funcName = { "BundAnimation", "UpAnimation", "RotateAnimation" };

    public int i { private get; set; }

    public void Chin()
    {
        Invoke(funcName[i], 0);
    }



    public void BundAnimation()
    {
        
        startTime = Time.time;
        text.ForceMeshUpdate();
        
        if(text.textInfo.characterCount==0)
        {
            return;
        }

        cachedMeshInfo = text.textInfo.CopyMeshInfoVertexData();

        for (int i=0;i<text.textInfo.characterCount;i++)
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
            
            x = ((velocity*Time.time) -( i* delay * 9.8f / (Time.time+0.001f)* (Time.time+0.0001f))/2)/frame;

            anim = curve.Evaluate(x)/5.0f;
            scale.x = scaleAnimationX.Evaluate(x);
            scale.y = scaleAnimationY.Evaluate(x);
            scale.z = 1.0f;
            rot.z = rotateAnimation.Evaluate(x);

            verts[vertexIndex + 0] +=- centerVec;
            verts[vertexIndex + 1] +=- centerVec;
            verts[vertexIndex + 2] +=- centerVec;
            verts[vertexIndex + 3] +=- centerVec;
            
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

        for(int i=0;i<text.textInfo.materialCount;i++)
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

            x = ((velocity * Time.time) - (i * delay * 9.8f / (Time.time + 0.001f) * (Time.time + 0.0001f)) / 2) / frame;

            anim = curve.Evaluate(x) / 5.0f;
            scale.x = scaleAnimationX.Evaluate(x);
            scale.y = scaleAnimationY.Evaluate(x);
            scale.z = 1.0f;

            if (i%2==0)
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

            Vector3 zeroDegreePoint = Vector3.up * 2.0f;
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
