using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade_Anime : MonoBehaviour
{
    public IEnumerator FadeIn()
    {
        Image _Image = GetComponent<Image>();
        _Image.material = Resources.Load<Material>("Materials/FadeIn");
        yield return FadeAnime(_Image.material, 3);
        SceneManager.LoadScene("Text");
    }
    public IEnumerator FadeOut()
    {
        Image _Image = GetComponent<Image>();
        _Image.material = Resources.Load<Material>("Materials/FadeOut");
        yield return FadeAnime(_Image.material, 3);
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
