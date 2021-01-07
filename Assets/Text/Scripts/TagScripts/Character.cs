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
    private Vector3 _position;
    private Vector3 _scale;
    private GameObject canvas;

    public string Name { get; private set; }

    public void Init(string name)
    {
        _position = new Vector3(0f, 0f, 0f);
        _scale = new Vector3(1f, 1f, 1f);
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
        _position.x = x;
        _position.y = y;
        _scale.x = scale;
        _scale.y = scale;
        FadeIn();
    }

    public void Move()
    {
        action = true;
    }
    void Update()
    {
        if (action)
        {
            float step = _speed * Time.deltaTime;
            _characterImage.rectTransform.position += new Vector3(-step, 0, 0);
        }
        else
        {
            _characterImage.rectTransform.localPosition = _position;
            _characterImage.rectTransform.localScale = _scale;
        }
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
