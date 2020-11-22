using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class CharacterIcon : MonoBehaviour {

    private GameObject _charaIcoObject;
    private SpriteRenderer _charaIcoImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private CanvasGroup _canvasGroup;

    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _charaIcoObject = gameObject;
        _charaIcoImage = _charaIcoObject.GetComponent<SpriteRenderer>();
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
        _charaIcoImage.sprite = _sprites[imageID];
        FadeIn();
    }

    public void Appear()
    {
        _charaIcoObject.SetActive(true);
        FadeIn();
    }

    public void FadeIn()
    {
        _charaIcoImage.color = new Color(1f, 1f, 1f, 0);
        _charaIcoImage.DOFade(1.0f, 0.2f);
    }

    public void Destroy()
    {
        Destroy(this);
    }


}
