using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    private GameObject _characterObject;
    private Image _characterImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private float _speed =1.0f;
    public bool action = false;
    public Vector3 _position;
    public Vector3 _scale;
    private GameObject canvas;

    public string Name { get; private set; }

    public void Init(string name)
    {
        
        this.Name = name;
        _characterObject = gameObject;
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterImage")
            {
                _characterImage = child.gameObject.GetComponent<Image>();
            }
        }
       
        gameObject.SetActive(false);
        LoadImage();
        _characterImage.color = new Color(1f, 1f, 1f, 1f);
    }

    public void LoadImage()
    {
        var temp = Resources.LoadAll<Sprite>("Image/" + Name).ToList();
        foreach (Sprite s in temp)
        {
            _sprites.Add(s.name, s);
        }
    }

    public void SetImage(string imageID, float x, float y, float scale)
    {
        _characterImage.sprite = _sprites[imageID];
        _characterImage.rectTransform.localPosition = new Vector3(x,y,0.0f);
        _characterImage.rectTransform.localScale = new Vector3(scale, scale,1);
       
        //FadeIn();
    }

    public void Move()
    {
        action = true;
    }
    void Update()
    {
    }

    public void Appear()
    {
        _characterObject.SetActive(true);
    }

    public void FadeIn()
    {
        _characterImage.color = new Color(1f, 1f, 1f, 0);
        _characterImage.DOFade(1.0f, 0.2f);
    }

    public void Destroy()
    {
        Destroy(this);
    }


}
