using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneController
{
    public Actions Actions;
    public List<CharacterIcon> CharaIcons = new List<CharacterIcon>();//このように書きながらも結局インスタンス一つしか生成してないです(簡単に複数にも出来ます)
    public List<Character> Characters = new List<Character>();    //このように書きながらも結局インスタンス一つしか生成してないです(簡単に複数にも出来ます)
    public List<Background> Backgrounds = new List<Background>();   //このように書きながらも結局インスタンス一つしか生成してないです(簡単に複数にも出来ます)
    public List<Score> Scores = new List<Score>();
    public List<Fade> Fade = new List<Fade>();
    private Sound _Sound;
    public string sceneTxtname { private get; set; }

    private GameController _gc;
    private GUIManager _gui;
    private SceneHolder _sh;
    private SceneReader _sr;
    private Sequence _textSeq  = DOTween.Sequence();
    private Sequence _imageSeq = DOTween.Sequence();
    private Scene _currentScene;
    private bool _isOptionsShowed;
    private bool _isSaveShowed;
    private string _tempText;
    private float _messageSpeed = 0.1f;
    private bool _charaAction = false;
    private GameObject canvas;
    private GameObject targetGameObject;

    public SceneController(GameController _gc)
    {
        this._gc = _gc;
        _gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        Actions = new Actions(_gc);
        _sh = new SceneHolder(this);
        _sr = new SceneReader(this);
        _textSeq.Complete();
        sceneTxtname = "";

        //ここはセーブのウィンドウが開いているか開いていないかでテキストのクリックを止める処理
        canvas = GameObject.Find("Canvas");
        foreach (Transform child in canvas.transform)
        {
            if (child.name == "SaveLoadWindow")
            {
                targetGameObject = child.gameObject;
            }
        }
    }

    public void WaitClick()
    {
        if (_currentScene != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //  キャラの座標戻し
                if (_charaAction)
                {
                    var character = Characters.Find(c => c.Name == "Characters");
                    character.action = false;
                    _charaAction = false;
                }

                //ここでタップ判定(テキストを進めていいのかどうか)
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
                    if (collition2d != null|| targetGameObject.activeSelf)
                    {
                        return;
                    }
                }


                if (!_isOptionsShowed && !_imageSeq.IsPlaying() &&!_isSaveShowed)
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
        _currentScene = _sh.Scenes.Find(s => s.ID == id+sceneTxtname);
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


    //特別なアクション処理//////////////////////////////////////////////////
    public void Action(string num)
    {
        int num2 = int.Parse(num);
        Actions.SelectAction(num2);
    }

    public void ActionMove()
    {
        var character = Characters.Find(c => c.Name == "Characters");
        _charaAction = true;
        character.Move();
    }

    /////////////////////////////////////////////////////////////////////


    //キャラクター処理//////////////////////////////////////////////////
    public void SetCharacter(string name)
    {
        Characters.ForEach(c => c.Destroy());
        Characters = new List<Character>();
        AddCharacter(name);
    }
    public void AddCharacter(string name)
    {
        if (Characters.Exists(c => c.Name == name)) return;

        var prefab = Resources.Load("Prefabs/Character") as GameObject;
        var charactorObject = Object.Instantiate(prefab);
        var character = charactorObject.GetComponent<Character>();

        character.Init(name);
        Characters.Add(character);
        _imageSeq = DOTween.Sequence();

        var cpos = new Vector3(0, _gui.MainCamera.transform.position.y, 0);
        _imageSeq.Append(Characters[0].transform.DOMove(cpos, 0f))
            .OnComplete(() => character.Appear());
    }

    public void SetCharaImage(string name, string ID,float x,float y,float scale)
    {
        var character = Characters.Find(c => c.Name == name);
        character.SetImage(ID,x,y,scale);
    }
    //////////////////////////////////////////////////////////////////////

    //キャラアイコン処理//////////////////////////////////////////////////
    public void SetCharaIcon(string name)
    {
        CharaIcons.ForEach(c => c.Destroy());
        CharaIcons = new List<CharacterIcon>();
        AddCharaIcon(name);
    }
    public void AddCharaIcon(string name)
    {
        if (CharaIcons.Exists(c => c.Name == name)) return;

        var prefab = Resources.Load("Prefabs/CharacterIcon") as GameObject;
        var charactorObject = Object.Instantiate(prefab);
        var character = charactorObject.GetComponent<CharacterIcon>();

        character.Init(name);
        CharaIcons.Add(character);
    }
    public void SetIcoImage(string name, string ID)
    {
        var character = CharaIcons.Find(c => c.Name == name);
        character.SetImage(ID);
    }
    //////////////////////////////////////////////////////////////////////

    //フェード処理//////////////////////////////////////////////////
    public void SetFade(string name)
    {
        Fade.ForEach(c => c.Destroy());
        Fade = new List<Fade>();
        AddFade(name);
    }
    public void AddFade(string name)
    {
        if (Fade.Exists(c => c.Name == name)) return;

        var prefab = Resources.Load("Prefabs/Fade") as GameObject;
        var fadeObject = Object.Instantiate(prefab);
        var fade = fadeObject.GetComponent<Fade>();

        fade.Init(name);
        Fade.Add(fade);
    }
    public void SetFadeImage(string name, string ID)
    {
        var fade = Fade.Find(c => c.Name == name);
        fade.SetImage(ID);
    }
    //////////////////////////////////////////////////////////////////////

    //背景処理////////////////////////////////////////////////////////////
    public void SetBackground(string name)
    {
        Backgrounds.ForEach(c => c.Destroy());
        Backgrounds = new List<Background>();
        AddBackground(name);
    }
    public void AddBackground(string name)
    {
        if (Backgrounds.Exists(c => c.Name == name)) return;

        var prefab = Resources.Load("Prefabs/Background") as GameObject;
        var backgroundObject = Object.Instantiate(prefab);
        var background = backgroundObject.GetComponent<Background>();

        background.Init(name);
        Backgrounds.Add(background);
    }
    public void SetBgImage(string name, string ID)
    {
        var background = Backgrounds.Find(c => c.Name == name);
        background.SetImage(ID);
    }
    //////////////////////////////////////////////////////////////////////

    //スコア処理////////////////////////////////////////////////////////////
    public void AddScore(string name)
    {
        if (Scores.Exists(c => c.Name == name)) return;
        var prefab = Resources.Load("Prefabs/CharaScore") as GameObject;
        var scoreobject = Object.Instantiate(prefab);
        var score = scoreobject.GetComponent<Score>();
        score.Init(name);
        Scores.Add(score);
    }
    public void SetScore(string name, string num)
    {
        var score = Scores.Find(c => c.Name == name);
        int num2 = int.Parse(num);
        score.ChangeScore(num2);
    }
    //////////////////////////////////////////////////////////////////////

    //サウンド処理////////////////////////////////////////////////////////////
    public void AddSound()
    {
        var prefab = Resources.Load("Prefabs/SoundManager") as GameObject;
        var soundobject = Object.Instantiate(prefab);
        var sound = soundobject.GetComponent<Sound>();
        _Sound = sound;
        _Sound.Init();
    }
    public void ChangeBGM(string num)
    {
        int num2 = int.Parse(num);
        _Sound.ChangeBGM(num2);
    }
    public void ChangeSE(string num, string flag)
    {
        int num2 = int.Parse(num);
        int flag2 = int.Parse(flag);
        _Sound.ChangeSE(num2, flag2);
    }
    //////////////////////////////////////////////////////////////////////\
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