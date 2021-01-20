using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private GameObject _fadeObject;
    private Image _fadeImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private GameObject canvas;

    public string Name { get; private set; }
    public bool _playInfade { get; set; }
    public bool _playOutfade { get; set; }

    void Start()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _fadeObject = gameObject;
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == Name)
            {
                _fadeImage = child.gameObject.GetComponent<Image>();
            }
        }
        gameObject.SetActive(false);
        LoadImage();
    }

    public void LoadImage()
    {
        var temp = Resources.LoadAll<Sprite>("Image/" + Name).ToList();
        foreach (Sprite s in temp)
        {
            _sprites.Add(s.name, s);
        }
    }

    public void SetInImage(string imageID, int colorID)
    {
        _fadeImage.sprite = _sprites[imageID];
        _fadeImage.material = Resources.Load<Material>("Materials/FadeIn");
        _fadeImage.material.color = new Color(colorID, colorID, colorID, 0);
        StartCoroutine(FadeInStart());
    }
    public void SetOutImage(string imageID, int colorID)
    {
        _fadeImage.sprite = _sprites[imageID];
        _fadeImage.material = Resources.Load<Material>("Materials/FadeOut");
        _fadeImage.material.color = new Color(colorID, colorID, colorID, 1);
        StartCoroutine(FadeOutStart());
    }

    public void Appear()
    {
        _fadeObject.SetActive(true);
    }
    public void DisApp()
    {
        _fadeObject.SetActive(false);
    }

    public bool GetActive()
    {
        return _fadeObject.activeSelf;
    }

    public void FadeIn()
    {
        _fadeImage.color = new Color(1f, 1f, 1f, 0);
        _fadeImage.DOFade(1.0f, 0.0f);
    }

    public void FadeOut()
    {
        _fadeImage.color = new Color(1f, 1f, 1f, 0);
        _fadeImage.DOFade(0.0f, 1.0f);
    }

    public void Destroy()
    {
        Destroy(this);
    }

    public IEnumerator FadeInStart()
    {
        _playInfade = false;
        yield return FadeAnime(_fadeImage.material, 1);
        _playInfade = true;
        _playOutfade = false;
    }
    public IEnumerator FadeOutStart()
    {
        yield return FadeAnime(_fadeImage.material, 1);
        _playOutfade = false;
    }

    private IEnumerator FadeAnime(Material material, float time)
    {
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
