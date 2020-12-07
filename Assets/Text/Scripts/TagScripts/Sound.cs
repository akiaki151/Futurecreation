using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public AudioClip[] BGM;
    public AudioClip[] SE;
    private AudioSource[] _sources;

    int _se_index;
    int _bgm_index;
    private GameObject _soundObject;
 

    public string Name { get; private set; }

    public void Init()
    {
        _soundObject = gameObject;
        _sources = _soundObject.GetComponents<AudioSource>();
        Appear();

    }

    public void Appear()
    {
        _se_index = 0;
        _bgm_index = 0;
    }

    public void ChangeSE(int num,int flag)
    {
        bool judge = (flag == 1) ? true : false ;
        _se_index = num;
        if (_se_index!= 0)
        {
          
            _sources[0].loop = judge;
            _sources[0].clip = SE[_se_index - 1];
            _sources[0].Play();
            _se_index = 0;
        }
        else
         _sources[0].Stop();
    }

    public void ChangeBGM(int num)
    {
        _bgm_index = num;
        if (_bgm_index != 0)
        {
            _sources[1].clip = BGM[_bgm_index - 1];
            _sources[1].Play();
            _bgm_index = 0;
        }
    }

    public void ChangeCharacterVoice(int num)
    {
        _bgm_index = num;
        if (_bgm_index != 0)
        {
            _sources[2].clip = BGM[_bgm_index - 1];
            _sources[2].Play();
            _bgm_index = 0;
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
