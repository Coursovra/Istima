using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Управление движением игрока
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Slider _slider; 
    [SerializeField] private RectTransform _rectTransform; 
    [SerializeField] private PlayerSpriteController _playerSpriteController; 
    private readonly float _speed = .36f;
    Vector3 _worldDimensions;

    /// <summary>
    /// Сбрасывает позицию слайдера
    /// </summary>
    public void ResetSliderValue()
    {
        _slider.value = 0;
    }

    /// <summary>
    /// Конфигурация слайдера, установка настроек в зависимости от размера экрана и размера спрайта игрока
    /// </summary>
    private void Start()
    {
        _worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
        _slider.onValueChanged.AddListener(OnValueChanged);
        _slider.maxValue = _worldDimensions.x;
        _slider.minValue = -_worldDimensions.x;
        var skinView = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
        Vector2 spriteSize = skinView.SpriteRenderer.sprite.rect.size;
        _rectTransform.sizeDelta = new Vector2(spriteSize.x,_worldDimensions.y);
    }
    
    /// <summary>
    /// Отписка от события при уничтожении объекта игрока
    /// </summary>
    void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }
    
    /// <summary>
    /// Движение игрока по x-оси
    /// </summary>
    /// <param name="value">Значение слайдера</param>
    public void OnValueChanged(float value)
    {
        if (!PlayerController.IsPlaying) { return;}

        //print($"pos: {transform.position}, newValue: {value}, diff: {transform.position.x - value}");
        //if (transform.position.x - value < 2f) { return;} 
        transform.position = new Vector2(value, transform.position.y);
    }
}