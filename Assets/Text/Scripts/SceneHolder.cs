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
        Load();
    }

    public void Load()
    {
        var itemFile = Resources.Load("ScenarioData/csvTest") as TextAsset;
        var csvData = LoadCSV(itemFile);
        
        Scenes = Parse(csvData);
    }

    public List<string[]> LoadCSV(TextAsset file)
    {
        StringReader reader = new StringReader(file.text);
        var list = new List<string[]>();
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            list.Add(line.Split(','));         }
        return list;
    }

    public List<Scene> Parse(List<string[]> list)
    {
        var scenes = new List<Scene>();
        var scene = new Scene();

        for(int i=0;i<list.Count;i++)

        foreach (string line in list[i])
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
