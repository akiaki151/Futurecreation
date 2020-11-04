using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gratest : MonoBehaviour
{
    [SerializeField] private Gradient gradientColor;
    public int maxVisibleCharacters = 0;
    private TextMeshPro text;
    public float span = 1f;
    private float currentime = 0f;
    private float fonttime = 8f;
    public bool tate;

    private void Start()
    {
        maxVisibleCharacters = 0;
        this.text = GetComponent<TextMeshPro>();
       // this.text.text = "吾輩は<size=30>猫</size>である";
        var count = Mathf.Min(this.text.textInfo.characterCount, this.text.textInfo.characterInfo.Length);
    }

    private void Update()
    {
        currentime += Time.deltaTime;
        fonttime += Time.deltaTime;
        this.text.maxVisibleCharacters = this.maxVisibleCharacters;

        if (currentime > span)
        {
            // Debug.LogFormat("{0}秒経過", span);
            currentime = 0f;
            maxVisibleCharacters++;
        }

        Animation();
    }

    private void Animation()
    {
        this.text.ForceMeshUpdate(true);

        var count = Mathf.Min(this.text.textInfo.characterCount, this.text.textInfo.characterInfo.Length);
        //Debug.Log(count);

        for (int i = 0; i < count; i++)
        {
            var charInfo = this.text.textInfo.characterInfo[i];
            if (!charInfo.isVisible)
                continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Color32[] colors = this.text.textInfo.meshInfo[materialIndex].colors32;
            Vector3[] verts = this.text.textInfo.meshInfo[materialIndex].vertices;

            float timeOffset = -0.5f * i;
            float time1 = Mathf.PingPong(timeOffset + Time.realtimeSinceStartup, 1.0f);
            float time2 = Mathf.PingPong(timeOffset + Time.realtimeSinceStartup - 0.1f, 1.0f);

            colors[vertexIndex + 0] = gradientColor.Evaluate(time1);
            colors[vertexIndex + 1] = gradientColor.Evaluate(time1);
            colors[vertexIndex + 2] = gradientColor.Evaluate(time2);
            colors[vertexIndex + 3] = gradientColor.Evaluate(time2);

            float sinWaveOffset = 0.5f * i;
            float sinWave = Mathf.Sin(sinWaveOffset + Time.realtimeSinceStartup * Mathf.PI);
            verts[vertexIndex + 0].y += sinWave;
            verts[vertexIndex + 1].y += sinWave;
            verts[vertexIndex + 2].y += sinWave;
            verts[vertexIndex + 3].y += sinWave;

        }

        for (int i = 0; i < this.text.textInfo.materialCount; i++)
        {
            if (this.text.textInfo.meshInfo[i].mesh == null) { continue; }
            this.text.textInfo.meshInfo[i].mesh.colors32 = this.text.textInfo.meshInfo[i].colors32;
            this.text.textInfo.meshInfo[i].mesh.vertices = text.textInfo.meshInfo[i].vertices;
            this.text.UpdateGeometry(this.text.textInfo.meshInfo[i].mesh, i);

        }
    }
}
