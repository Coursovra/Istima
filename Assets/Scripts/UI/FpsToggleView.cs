using System;
using UnityEngine;
using UnityEngine.UI;

public class FpsToggleView : MonoBehaviour
{
    [SerializeField] private GameObject _fpsText;
    [SerializeField] private Toggle _fpsToggle;
    private void Start()
    {
        _fpsToggle.isOn = _fpsText.gameObject.activeSelf;
    }

    public void FpsToggle()
    {
        _fpsText.SetActive(_fpsToggle.isOn);
        PlayerPrefsController.SetFpsToggleStatus(Convert.ToInt32(_fpsToggle.isOn));
    }
}