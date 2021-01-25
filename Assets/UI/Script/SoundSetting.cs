using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixerObj;
    private Slider slider;

    private void Awake()
    {
        slider = transform.Find("Slider").GetComponentInChildren<Slider>();

        float temp = PlayerPrefs.GetInt(this.name);

        slider.value = (temp + 40) * 100 / 40;

        AudioMixerObj.SetFloat(this.name, (int)temp);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioMixerObj.SetFloat(this.name, PlayerPrefs.GetInt(this.name));
    }

    // Update is called once per frame
    void Update()
    {
        float temp;
        
        AudioMixerObj.GetFloat(this.name, out temp);

        float calc = slider.value / 100 * 40 - 40;

        if ((int)temp != (int)calc)
        {
            AudioMixerObj.SetFloat(this.name, calc);
            PlayerPrefs.SetInt(this.name, (int)calc);
        }
    }
}
