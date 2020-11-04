using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class SceneReader
{
    private SceneController _sc;
    private Sound _sound;
    private Actions _actions;

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

        if (line.Contains("#"))
        {
            while (true)
            {
                if (!line.Contains("#")) break;

                line = line.Replace("#", "");

                if (line.Contains("speaker"))
                {
                    line = line.Replace("speaker=", "");
                    _sc.SetSpeaker(line);
                }
                else if (line.Contains("chara"))
                {
                    line = line.Replace("chara=", "");
                    _sc.SetCharaImage("Characters", line);
                }
                else if (line.Contains("ico"))
                {
                    line = line.Replace("ico=", "");
                    _sc.SetIcoImage("CharaIcons", line);
                }
                else if (line.Contains("background"))
                {
                    line = line.Replace("background=", "");
                    _sc.SetBgImage("BackGrounds", line);
                }
                else if (line.Contains("score"))
                {
                    line = line.Replace("score=", "");
                    _sc.AddScore(line);
                    
                }
                else if (line.Contains("Action"))
                {
                    line = line.Replace("Action=", "");
                    _sc.Action(line);

                }
                else if (line.Contains("SE"))
                {
                    line = line.Replace("SE=", "");
                    _sc.ChangeSE(line);

                }
                else if (line.Contains("BGM"))
                {
                    line = line.Replace("BGM=", "");
                    _sc.ChangeBGM(line);

                }
                else if (line.Contains("add"))
                {
                    line = line.Replace("add=", "");
                    var splitted = line.Split(':');
                    _sc.SetScore(splitted[0], splitted[1]);
                }
                else if (line.Contains("next"))
                {
                    line = line.Replace("next=", "");
                    _sc.SetScene(line);
                }
                else if (line.Contains("method"))
                {
                    line = line.Replace("method=", "");
                    var type = _actions.GetType();
                    MethodInfo mi = type.GetMethod(line);
                    mi.Invoke(_actions, new object[] { });
                }
                else if (line.Contains("options"))
                {
                    var options = new List<(string, string)>();
                    while (true)
                    {
                        s.GoNextLine();
                        line = line = s.Lines[s.Index];
                        if (line.Contains("["))
                        {
                            line = line.Replace("[", "").Replace("]", "");
                            var splitted = line.Split(':');
                            options.Add((splitted[0], splitted[1]));
                        }
                        else
                        {
                            _sc.SetOptions(options);
                            break;
                        }
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