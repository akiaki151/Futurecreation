using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneHolder
{
    public List<Scene> Scenes = new List<Scene>();
    private SceneController _sc;

    public SceneHolder(SceneController _sc)
    {
        this._sc = _sc;
        Load("SelectScenario");
    }

    private void Load(string name)
    {
        var itemFile = Resources.Load("ScenarioData/"+name) as TextAsset;
        var textData = LoadText(itemFile);
        Scenes = Parse(textData);
        
    }

    private List<string> Load2(string name)
    {
        var itemFile = Resources.Load("ScenarioData/" + name) as TextAsset;
        var textData = LoadText2(itemFile,name);
        return textData;
    }

    public List<string> LoadText(TextAsset file)
    {
        StringReader reader = new StringReader(file.text);
        var list = new List<string>();
        var list2 = new List<string>();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            if (line.Contains("#Setscenario"))
            {
                line = line.Replace("#Setscenario=", "");
                list2.AddRange(Load2(line));
            }
            else
            {
                list.Add(line);
            }

        }
        list.AddRange(list2);
        return list; 
    }

    public List<string> LoadText2(TextAsset file,string name)
    {
        StringReader reader = new StringReader(file.text);
        var list = new List<string>();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            if (line.Contains("#scene"))
            {

                line +=name;
            }
            list.Add(line);
        }
        return list;
    }

    public List<Scene> Parse(List<string> list)
    {
        var scenes = new List<Scene>();
        var scene = new Scene();
        foreach (string line in list)
        {
            if (line.Contains("#scene"))
            {
                var ID = line.Replace("#scene=", "");
                
                scene = new Scene(ID);
                scenes.Add(scene);
            }
            else
            {
                scene.Lines.Add(line);
            }
        }
        return scenes;
    }


}
