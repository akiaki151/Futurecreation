using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Fade : MonoBehaviour {

    private GameObject _fadeObject;
    private SpriteRenderer _fadeImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private CanvasGroup _canvasGroup;

    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _fadeObject = gameObject;
        _fadeImage = _fadeObject.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
        LoadImage();
    }

    public void LoadImage()
    {
        var temp = Resources.LoadAll<Sprite>("Image/"+Name).ToList();
        foreach (Sprite s in temp)
        {
            _sprites.Add(s.name, s);
        }
    }

    public void SetImage(string imageID)
    {
        _fadeImage.sprite = _sprites[imageID];
        FadeIn();
        FadeOut();
    }

    public void Appear()
    {
        _fadeObject.SetActive(true);
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


}
