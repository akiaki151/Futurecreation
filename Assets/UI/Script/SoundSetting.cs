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
        slider = transform.FindChild("Slider").GetComponent<Slider>();

        slider.value = Mathf.Abs(PlayerPrefs.GetInt(this.name)) / 40.0f * 100.0f;

        float calc = (slider.value - 100.0f) / 100.0f * 40.0f;

        AudioMixerObj.SetFloat(this.name, calc);
        PlayerPrefs.SetInt(this.name, (int)calc);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float temp;

        AudioMixerObj.GetFloat(this.name, out temp);

        float calc = (slider.value - 100.0f) / 100.0f * 40.0f;

        if ((int)temp != (int)calc)
        {
            AudioMixerObj.SetFloat(this.name, calc);
            PlayerPrefs.SetInt(this.name, (int)calc);
        }
    }
}
