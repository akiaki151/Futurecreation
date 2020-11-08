using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tag;

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
                                "Options"
                                };
    
    public SceneReader(SceneController _sc)
    {
        this._sc = _sc;
        _actions = _sc.Actions;
        _sc.AddBackground("BackGrounds");
        _sc.AddCharaIcon("CharaIcons");
        _sc.AddCharacter("Characters");
        _sc.AddSound();
    }

    public void ReadLines(Scene s)
    {
        if (s.Index >= s.Lines.Count) return;
        var line = s.GetCurrentLine();
        var text = "";
        var list = new List<string>();
        list.AddRange(_taglist);
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

                        _tagFactory.CreateTag(tag).Do(_sc,line,s);
                        
                    }
                }
               
                s.GoNextLine();
                if (s.IsFinished()) break;
                line = s.GetCurrentLine();
            }
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