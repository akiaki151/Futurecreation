using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPAnimationLoop : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve positionY;
    [SerializeField]
    private AnimationCurve scaleAnimationX;
    [SerializeField]
    private AnimationCurve scaleAnimationY;
    [SerializeField]
    private AnimationCurve rotateAnimation;

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
    public int frame;
    public int velocity;
    public int delay;
    public float repeatTime;

    private float nowDegree;

    private TextMeshProUGUI text;

    private string[] funcName = { "NormalAnimation", "UpAnimation", "RotateAnimation" };

    public int i;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        Invoke(funcName[i], 0);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(funcName[i], 0);
    }

    public void NormalAnimation()
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

            x = ((velocity * Mathf.Repeat(Time.time, repeatTime)) - (i * delay * 9.8f / (Mathf.Repeat(Time.time, repeatTime) + 0.001f) * (Mathf.Repeat(Time.time, repeatTime) + 0.0001f)) / 2) / frame;

            anim = positionY.Evaluate(x) * 3.0f;
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
}
