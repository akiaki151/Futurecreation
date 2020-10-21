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
    private Vector3 direction = new Vector3(-20f, 0f, 0f);
    private float speed =2.5f;
    public bool action = false;

    public string Name { get; private set; }

    void Start()
    {

    }

    public void Init(string name)
    {
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

    public void SetImage(string imageID)
    {
        _characterImage.sprite = _sprites[imageID];
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
            float step = speed * Time.deltaTime;
            _characterObject.transform.position = Vector3.MoveTowards(transform.position, direction, step);
        }
        else
        {
            _characterObject.transform.position = new Vector3(0f, 0f, 0f);
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
