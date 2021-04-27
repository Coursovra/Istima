using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool IsPlaying;
    [SerializeField] private UIController _uiController;
    private void OnMouseDown()
    {
        if (!IsPlaying)
        {
            _uiController.SwitchUi(true);
            IsPlaying = true;
        }
    }
}
