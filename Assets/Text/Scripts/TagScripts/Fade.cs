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
    private Image _backImage;
    private Sprite _backsprite;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private GameObject canvas;

    public string Name { get; private set; }
    public bool _playfade { get; set; }

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
            if (child.name == "Background")
            {
                _backImage = child.gameObject.GetComponent<Image>();
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

    public void SetImage(string imageID, string BackimageID)
    {
        _fadeImage.sprite = _sprites[imageID];
        StartCoroutine(FadeStart());
        _backsprite = Resources.Load<Sprite>("Image/" + "Backgrounds/" + BackimageID);
        //FadeIn();
        //FadeOut();
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

    public IEnumerator FadeStart()
    {
        _playfade = true;
        _fadeImage.material = Resources.Load<Material>("Materials/FadeIn");
        yield return FadeAnime(_fadeImage.material, 1);
        _backImage.sprite = _backsprite;
        _fadeImage.material = Resources.Load<Material>("Materials/FadeOut");
        yield return FadeAnime(_fadeImage.material, 1);
        _playfade = false;
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
