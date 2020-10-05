using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Character : MonoBehaviour {

    private GameObject _charactorObject;
    private SpriteRenderer _charactorImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private CanvasGroup _canvasGroup;

    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _charactorObject = gameObject;
        _charactorImage = _charactorObject.GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
        LoadImage();
    }

    public void LoadImage()
    {
        var temp = Resources.LoadAll<Sprite>("Image/Characters/"+Name).ToList();
        foreach (Sprite s in temp)
        {
            _sprites.Add(s.name, s);
        }
    }

    public void SetImage(string imageID)
    {
        _charactorImage.sprite = _sprites[imageID];
        FadeIn();
    }

    public void Appear()
    {
        _charactorObject.SetActive(true);
        FadeIn();
    }

    public void FadeIn()
    {
        _charactorImage.color = new Color(1f, 1f, 1f, 0);
        _charactorImage.DOFade(1.0f, 0.2f);
    }

    public void Destroy()
    {
        Destroy(this);
    }


}
