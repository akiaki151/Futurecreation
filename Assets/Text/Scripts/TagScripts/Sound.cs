using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Sound : MonoBehaviour
{

    //public AudioClip[] BGM;
    private List<AudioClip> BGM = new List<AudioClip>();
    private List<AudioClip> SE = new List<AudioClip>();
    //public AudioClip[] SE;
    private AudioSource[] _sources;
    private GameObject _soundObject;

    // Multilineはインスペクタの文字列を複数行で表示します
    [SerializeField, Multiline(5)] private string[] str;

    void ReadFiles()
    {
        var temp = Resources.LoadAll<AudioClip>("Sounds/BGM");
        foreach (AudioClip s in temp)
        {
            BGM.Add(s);
        }

        var temp2 = Resources.LoadAll<AudioClip>("Sounds/SE");
        foreach (AudioClip s in temp2)
        {
            SE.Add(s);
        }
    }

    public void Init()
    {
        ReadFiles();
        _soundObject = gameObject;
        _sources = _soundObject.GetComponents<AudioSource>();
       
    }

    public void ChangeSE(string name,int flag)
    {
        bool judge = (flag == 1) ? true : false;
        for (int i = 0; i < SE.Count; i++)
        {
            if (SE[i].name == name)
            {
                _sources[0].loop = judge;
                _sources[0].clip = SE[i];
                _sources[0].Play();
                break;
            }
            else
            {
                _sources[0].Stop();
            }
        }
    }

    public void ChangeBGM(string name)
    {   
        for (int i=0;i< BGM.Count;i++)
        {
            if (BGM[i].name == name)
            {
                
                _sources[1].clip = BGM[i]; 
                _sources[1].Play();
                break;
            }
            else
            {
                _sources[1].Stop();
            }
        }
    }

    public void ChangeCharacterVoice(int num)
    {
        //_bgm_index = num;
        //if (_bgm_index != 0)
        //{
        //    _sources[2].clip = BGM[_bgm_index - 1];
        //    _sources[2].Play();
        //    _bgm_index = 0;
        //}
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
