using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class SceneReader
{
    private SceneController _sc;
    private Actions _actions;

    public SceneReader(SceneController _sc)
    {
        this._sc = _sc;
        _actions = _sc.Actions;
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
                    _sc.AddCharactor(line);
                }
                else if (line.Contains("image"))
                {
                    line = line.Replace("image_", "");
                    var splitted = line.Split('=');
                    _sc.SetImage(splitted[0], splitted[1]);
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
                        if (line.Contains("{"))
                        {
                            line = line.Replace("{", "").Replace("}", "");
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