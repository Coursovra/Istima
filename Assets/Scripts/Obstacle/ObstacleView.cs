using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Вью препятствия
/// </summary>
public class ObstacleView : MonoBehaviour
{
    public float HitPoints { get; set; }

    public int ScoreForHit
    {
        get => _scoreForHit;
        set => _scoreForHit = value;
    }
    //[SerializeField] private Material _material;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private int _scoreForHit;
    public event Action OnPlayerDeath;
    private BoxCollider2D _boxCollider2D;
    //public event Action<float> OnTakeDamage;

    private void Start()
    {
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }
    
    /// <summary>
    /// Установка цвета препятствия в зависимости от количества ХП
    /// </summary>
    /// <param name="hitPoints"> Количество ХП </param>
    public void SetHitPoints(float hitPoints)
    {
        HitPoints = hitPoints;
        _hpText.text = hitPoints.ToString();
        // _material.color = hitPoints switch //todo: ?почему не работает?
        // {
        //     <= 50 => Color.green,
        //     <= 100 => Color.blue,
        //     <= 200 => Color.red,
        //     _ => _material.color
        // };

        if (hitPoints <= 50)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green; //todo: to var
        }
        if (hitPoints > 50)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (hitPoints > 100)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (hitPoints > 200)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if (hitPoints <= 0)
        {
            DisableObstacle();
        }
    }

    /// <summary>
    /// Отключение препятствия (тригера).
    /// </summary>
    private void DisableObstacle()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(transform.GetChild(0).gameObject);
    }
    
    /// <summary>
    /// Обработка смерти игрока, при попадании его в триггер препятствия
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController.IsPlaying = false;
            OnPlayerDeath?.Invoke();
        }
    }

    /// <summary>
    /// Уничтожение препятствий, которые не видит игрок (За экраном).
    /// </summary>
    private void OnBecameInvisible()
    {
        if(Camera.main == null) { return; }
        float _distance = -8.0f;
        var frustumHeight = 2.0f * _distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        if (transform.position.y < frustumHeight)
        {
            Destroy(gameObject);
        }
    }
    
}
