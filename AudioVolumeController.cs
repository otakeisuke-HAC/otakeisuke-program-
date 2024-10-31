using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer m_BGMAudioMixer;
    [SerializeField]
    private AudioMixer m_SEAudioMixer;
    [SerializeField]
    private Slider m_BGMVolumeSlider;
    [SerializeField]
    private Slider m_SEVolumeSlider;

    AudioSource m_AudioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_BGMVolumeSlider.value = 0.8f;
        m_SEVolumeSlider.value = 0.8f;
        if (m_BGMVolumeSlider != null)
        {
            m_BGMVolumeSlider.onValueChanged.AddListener((value) =>
            {
                // valueは0～1の値を期待する。それを保証するための処理
                value = Mathf.Clamp01(value);

                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, -80f, 20f);
                m_BGMAudioMixer.SetFloat("BGM-Volume", decibel);
            });
        }

        if (m_SEVolumeSlider != null)
        {
            m_SEVolumeSlider.onValueChanged.AddListener((value) =>
            {
                // valueは0～1の値を期待する。それを保証するための処理
                value = Mathf.Clamp01(value);

                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, -80f, 20f);
                m_SEAudioMixer.SetFloat("SE-Volume", decibel);
            });
        }
    }

    public void SEChcek()
    {
        m_AudioSource.Play();
    }
}
