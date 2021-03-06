﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    private GameObject _bgObject;
    private Image _bgImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private GameObject canvas;

    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _bgObject = gameObject;
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "Background")
            {
                _bgImage = child.gameObject.GetComponent<Image>();
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

    public void SetImage(string imageID)
    {
        _bgImage.sprite = _sprites[imageID];
        FadeIn();
    }

    public void Appear()
    {
        _bgObject.SetActive(true);
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
