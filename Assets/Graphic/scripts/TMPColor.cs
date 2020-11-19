using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPColor : MonoBehaviour
{
    public bool gradation;
    [SerializeField] private Gradient gradientColor;
    private TextMeshPro text;
    public Color32 color;
    // Start is called before the first frame update
    void Start()
    {
        this.text = GetComponent<TextMeshPro>();
      
    }

    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            var charaInfo = text.textInfo.characterInfo[i];

            if (!charaInfo.isVisible)
            {
                continue;       //無効な文字スキップ
            }

            int materialIndex = charaInfo.materialReferenceIndex;
            int vertexIndex = charaInfo.vertexIndex;
            Color32[] colors = text.textInfo.meshInfo[materialIndex].colors32;

            for (int y = 0; y < 4; y++)
            {
                if (gradation)
                {
                    colors[vertexIndex + y] = gradientColor.Evaluate(1);
                }
                else
                {
                    colors[vertexIndex + y] = color;
                }

            }
        }

        for (int i = 0; i < text.textInfo.materialCount; i++)
        {
            if (this.text.textInfo.meshInfo[i].mesh == null)
            {
                continue;
            }
            text.textInfo.meshInfo[i].mesh.colors32 = text.textInfo.meshInfo[i].colors32;
            text.UpdateGeometry(text.textInfo.meshInfo[i].mesh, i);
        }
    }
}
