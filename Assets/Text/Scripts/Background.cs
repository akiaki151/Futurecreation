using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    private GameObject _bgObject;
    private Image _bgImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private Canvas canvas;
    private CanvasScaler canvasscaler;
    public string Name { get; private set; }
    
    void Start ()
    {
        

    }

    public void Init(string name)
    {
        this.Name = name;
        _bgObject = gameObject;
        _bgImage = _bgObject.GetComponent<Image>();
        gameObject.SetActive(false);

        canvas = _bgObject.GetComponent<Canvas>();
        canvasscaler = _bgObject.GetComponent<CanvasScaler>();
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

    public void SetImage(string imageID)
    {
        _bgImage.sprite = _sprites[imageID];
        FadeIn();
    }

    public void Appear()
    {
        _bgObject.SetActive(true);
        FadeIn();
    }

    public void FadeIn()
    {
        _bgImage.color = new Color(1f, 1f, 1f, 0);
        _bgImage.DOFade(1.0f, 0.0f);
        
    }

    public void Destroy()
    {
        Destroy(this);
    }


}
