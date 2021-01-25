using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Material m_FadeIn;
    public Material m_FadeOut;
    public Sprite NewTextuer;
    public bool m_Eneble;

    void Start()
    {
        m_Eneble = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !m_Eneble)
        {
            StartCoroutine(TransitionIn());
        }
        if (Input.GetKeyDown(KeyCode.Space) && m_Eneble)
        {
            StartCoroutine(TransitionOut());
        }
    }
    IEnumerator TransitionIn()
    {
        Image _Image = GetComponent<Image>();
        _Image.material = Resources.Load<Material>("Materials/FadeIn");
        yield return FadeAnime(_Image.material, 1);
        GameObject.Find("TextuerPanel").GetComponent<Image>().sprite = NewTextuer;
        m_Eneble = true;
    }
    IEnumerator TransitionOut()
    {
        Image _Image = GetComponent<Image>();
        _Image.material = Resources.Load<Material>("Materials/FadeOut");
        yield return FadeAnime(_Image.material, 1);
        m_Eneble = false;
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