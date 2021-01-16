using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Material m_FadeIn;
    public Material m_FadeOut;
    public Sprite NewTextuer;

    void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Transition());
        }
    }
    IEnumerator Transition()
    {
        Image _Image = GetComponent<Image>();
        _Image.material = Resources.Load<Material>("Materials/FadeIn");
        yield return FadeAnime(_Image.material, 1);
        GameObject.Find("TextuerPanel").GetComponent<Image>().sprite = NewTextuer;
        _Image.material = Resources.Load<Material>("Materials/FadeOut");
        yield return FadeAnime(_Image.material, 1);
    }

    IEnumerator FadeAnime(Material material, float time)
    {
        GetComponent<Image>().material = material;
        float current = 0;
        while (current < time)
        {
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        material.SetFloat("_Alpha", 1);
    }
}