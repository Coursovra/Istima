using System;
using TMPro;
using UnityEngine;

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

    public void SetHitPoints(int hitPoints)
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

    private void DisableObstacle()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(transform.GetChild(0).gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController.IsPlaying = false;
            OnPlayerDeath?.Invoke();
        }
    }
}
