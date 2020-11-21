using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
public class CharacterIcon : MonoBehaviour {

    private GameObject _charaIcoObject;
    private Image _charaIcoImage;
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
        _charaIcoObject = gameObject;
        _charaIcoImage = _charaIcoObject.GetComponent<Image>();
        gameObject.SetActive(false);
        canvas = _charaIcoObject.GetComponent<Canvas>();
        canvasscaler = _charaIcoObject.GetComponent<CanvasScaler>();
        canvas.sortingOrder = 10;
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
