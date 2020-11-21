using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
public class Fade : MonoBehaviour {

    private GameObject _fadeObject;
    private Image _fadeImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private CanvasGroup _canvasGroup;
    private Canvas canvas;
    private CanvasScaler canvasscaler;
    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _fadeObject = gameObject;
        _fadeImage = _fadeObject.GetComponent<Image>();
        gameObject.SetActive(false);
        canvas = _fadeObject.GetComponent<Canvas>();
        canvasscaler = _fadeObject.GetComponent<CanvasScaler>();
        canvas.sortingOrder = -999;
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
