using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Character : MonoBehaviour
{

    private GameObject _characterObject;
    private SpriteRenderer _characterImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private CanvasGroup _canvasGroup;
    private Vector3 _direction = new Vector3(-20f, 0f, 0f);
    private float _speed =2.5f;
    public bool action = false;
    private Vector3 _position;
    private Vector3 _scale;

    public string Name { get; private set; }

    public void Init(string name)
    {
        _position = new Vector3(0f, 0f, 0f);
        _scale = new Vector3(1f, 1f, 1f);
        this.Name = name;
        _characterObject = gameObject;
        _characterImage = _characterObject.GetComponent<SpriteRenderer>();
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
            _characterObject.transform.position = Vector3.MoveTowards(transform.position, _direction, step);
        }
        else
        {
            _characterObject.transform.position = _position;
            _characterObject.transform.localScale = _scale;
        }
    }

    public void Appear()
    {
        _characterObject.SetActive(true);
        FadeIn();
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
