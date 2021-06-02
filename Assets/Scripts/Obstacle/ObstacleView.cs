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

    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private int _scoreForHit;
    [SerializeField] private ParticleSystem _particleSystemRed;
    [SerializeField] private ParticleSystem _particleSystemGreen;
    [SerializeField] private ParticleSystem _particleSystemBlue;
    [SerializeField] private ParticleSystem _particleSystemYellow;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    private ParticleSystem _currentParticleSystem;
    public static event Action OnPlayerDeath;

    
    /// <summary>
    /// Установка цвета препятствия в зависимости от количества ХП
    /// </summary>
    /// <param name="hitPoints"> Количество ХП </param>
    public void SetHitPoints(int hitPoints) //todo: refactoring?
    {
        HitPoints = hitPoints;
        _hpText.text = hitPoints.ToString();
        // _material.color = hitPoints switch
        // {
        //     <= 50 => Color.green,
        //     <= 100 => Color.blue,
        //     <= 200 => Color.red,
        //     _ => _material.color
        // };

        if (gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            _currentParticleSystem = _particleSystemRed;
        }
        else if (gameObject.GetComponent<Renderer>().material.color == Color.yellow)
        {
            _currentParticleSystem = _particleSystemYellow;
        }
        else if (gameObject.GetComponent<Renderer>().material.color == Color.blue)
        {
            _currentParticleSystem = _particleSystemBlue;
        }
        else if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            _currentParticleSystem = _particleSystemGreen;
        }

        if (hitPoints <= 50)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
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
            var particlePrefab = Instantiate(_currentParticleSystem, transform);
            Destroy(particlePrefab, 1.2f);
            DisableObstacle();
        }
        
        if (hitPoints.ToString().Length <= 4)
            _hpText.fontSize = .4f;
        else if (hitPoints.ToString().Length == 5)
            _hpText.fontSize = .3f;
        else if (hitPoints.ToString().Length == 6)
            _hpText.fontSize = .25f;
        else if (hitPoints.ToString().Length == 7)
            _hpText.fontSize = .2f;
        else if (hitPoints.ToString().Length < 11)
            _hpText.fontSize = .15f;
        else if (hitPoints.ToString().Length > 11)
            _hpText.fontSize = .1f;
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
            _audioSource.PlayOneShot(_audioClip);
            
            PlayerController.IsPlaying = false;
            OnPlayerDeath?.Invoke();
        }
    }
}
