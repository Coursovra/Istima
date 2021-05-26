using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderView : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _mixerGroupName;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;

    private void Start()
    {
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        _audioMixer.GetFloat(_mixerGroupName, out var value);
        _slider.value = value;
    }

    public void OnValueChanged()
    {
        _audioMixer.SetFloat(_mixerGroupName, _slider.value);
    }
}
