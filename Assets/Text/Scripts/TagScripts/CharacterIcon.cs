﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour {

    private GameObject _charaIcoObject;
    private Image _charaIcoImage;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private GameObject canvas;

    public string Name { get; private set; }
    
    void Start ()
    {

    }

    public void Init(string name)
    {
        this.Name = name;
        _charaIcoObject = gameObject;
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "CharacterIcon")
            {
                _charaIcoImage = child.gameObject.GetComponent<Image>();
            }
        }
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
