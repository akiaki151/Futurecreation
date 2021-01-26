using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SceneReader 
{
    private SceneController _sc;
    private Sound _sound;
    private Actions _actions;
    private string[] _taglist = {"Speaker",
                                "Chara",
                                "Ico",
                                "Background",
                                "Score",
                                "Action",
                                "SE",
                                "BGM",
                                "Add",
                                "Next",
                                "Options",
                                "Getscenario",
                                //"Fade"
                                };
    private string[] _fadetag = {"FadeIn",
                                "FadeOut"
    };
    
    public SceneReader(SceneController _sc)
    {
        this._sc = _sc;
        _actions = _sc.Actions;
        _sc.SetBackground("BackGrounds");
        _sc.SetCharaIcon("CharaIcons");
        _sc.SetCharacter("Characters");
        _sc.SetFade("Fade");
        _sc.AddSound();
    }

    public void ReadLines(Scene s)
    {
        if (s.Index >= s.Lines.Count) return;
        var line = s.GetCurrentLine();
        var text = "";
        var list = new List<string>();
        list.AddRange(_taglist);
        var fade_list = new List<string>();
        fade_list.AddRange(_fadetag);
        var _tagFactory = new Taglist();

        if (line.Contains("#"))
        {
            while (true)
            {
                if (!line.Contains("#")) break;
                line = line.Replace("#", "");
                foreach (string tag in list)
                {
                    if (line.Contains(tag))
                    {

                        _tagFactory.CreateTag(tag).Do(_sc, line, s);

                    }
                }
                s.GoNextLine();
                if (s.IsFinished()) break;
                line = s.GetCurrentLine();
            }
        }
        if (line.Contains("*"))
        {
            line = line.Replace("*", "");
            foreach (string tag in fade_list)
            {
                if (line.Contains(tag))
                {
                    _tagFactory.CreateTag(tag).Do(_sc, line, s);
                }
            }
            s.GoNextLine();
            line = s.GetCurrentLine();
        }

        if (line.Contains('{'))
        {
            line = line.Replace("{", "");
            while (true)
            {
                if (line.Contains('}'))
                {
                    line = line.Replace("}", "");
                    text += line;
                    SceneController.LogText += text + "\n\n";
                    s.GoNextLine();
                    break;
                }
                else
                {
                    text += line;
                }
                s.GoNextLine();
                if (s.IsFinished()) break;
                line = s.GetCurrentLine();
            }
            if (!string.IsNullOrEmpty(text)) _sc.SetText(text);
        }
    }
}