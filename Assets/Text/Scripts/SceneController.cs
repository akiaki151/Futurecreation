using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneController
{  
    public Actions Actions;
    public List<Character> Characters = new List<Character>();

    private GameController _gc;
    private GUIManager _gui;
    private SceneHolder _sh;
    private SceneReader _sr;
    private Sequence _textSeq = DOTween.Sequence();
    private Sequence _imageSeq = DOTween.Sequence();
    private Scene _currentScene;
    private bool _isOptionsShowed;
    private string _tempText;
    private float _messageSpeed = 0.1f;

    public SceneController(GameController _gc)
    {
        this._gc = _gc;
        _gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        Actions = new Actions(_gc);
        _sh = new SceneHolder(this);
        _sr = new SceneReader(this);
        _textSeq.Complete();
    }

    public void WaitClick()
    {
        if (_currentScene != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);

                    if (collition2d != null)
                    {
                        var button = collition2d.gameObject.GetComponent<Button>();
                        if (button != null) return;
                    }
                }

                if (!_isOptionsShowed && !_imageSeq.IsPlaying())
                {
                    SetNextProcess();
                }
            }
        }
    }

    public void SetComponents()
    {
        _gui.ButtonPanel.gameObject.SetActive(_isOptionsShowed);
        _gui.Delta.gameObject.SetActive
            (!_textSeq.IsPlaying() && !_isOptionsShowed && !_imageSeq.IsPlaying());
    }

    public void SetNextProcess()
    {
        if (_textSeq.IsPlaying())
        {
            SetText(_tempText);
        }
        else
        {
            _sr.ReadLines(_currentScene);
        }
    }

    public void SetScene(string id)
    {
        _currentScene = _sh.Scenes.Find(s => s.ID == id);
        _currentScene = _currentScene.Clone();
        if (_currentScene == null) Debug.LogError("scenario not found");
        SetNextProcess();
    }

    public void SetText(string text)
    {
        _tempText = text;
        if (_textSeq.IsPlaying())
        {
            _textSeq.Complete();
        }
        else
        {
            _gui.Text.text = "";
            _textSeq = DOTween.Sequence();
            _textSeq.Append
                (_gui.Text.DOText
                (
                    text,
                    text.Length * _messageSpeed
                ).SetEase(Ease.Linear));
        }
    }

    public void SetOptionsPanel()
    {
        _gui.ButtonPanel.gameObject.SetActive(_isOptionsShowed);
    }

    public void SetSpeaker(string name = "")
    {
        _gui.Speaker.text = name;
    }

    public void SetCharactor(string name)
    {
        Characters.ForEach(c => c.Destroy());
        Characters = new List<Character>();
        AddCharactor(name);
    }

    public void AddCharactor(string name)
    {
        if (Characters.Exists(c => c.Name == name)) return;

        var prefab = Resources.Load("Charactor") as GameObject;
        var charactorObject = Object.Instantiate(prefab);
        var character = charactorObject.GetComponent<Character>();

        character.Init(name);
        Characters.Add(character);
        _imageSeq = DOTween.Sequence();

        for (int i = 0; i < Characters.Count; i++)
        {
            var pos = _gui.MainCamera.ScreenToWorldPoint(Vector3.zero);
            var pos2 = _gui.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            var posWidth = pos2.x - pos.x;
            var left = pos.x + (posWidth * (i + 1) / (Characters.Count + 1));
            var cpos = new Vector3(0, _gui.MainCamera.transform.position.y, 0);

            _imageSeq.Append(Characters[i].transform.DOMove(cpos, 0f))
                .OnComplete(() => character.Appear());
        }

    }

    public void SetImage(string name, string ID)
    {
        var character = Characters.Find(c => c.Name == name);
        character.SetImage(ID);
    }

    public void SetOptions(List<(string text, string nextScene)> options)
    {
        _isOptionsShowed = true;
        foreach (var o in options)
        {
            Button b = Object.Instantiate(_gui.OptionButton);
            Text text = b.GetComponentInChildren<Text>();
            text.text = o.text;
            b.onClick.AddListener(() => onClickedOption(o.nextScene));
            b.transform.SetParent(_gui.ButtonPanel, false);
        }
    }

    public void onClickedOption(string nextID = "")
    {
        SetScene(nextID);
        _isOptionsShowed = false;
        foreach (Transform t in _gui.ButtonPanel)
        {
            UnityEngine.Object.Destroy(t.gameObject);
        }
    }

}