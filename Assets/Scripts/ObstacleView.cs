using System;
using TMPro;
using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    public float HitPoints { get; set; }
    //[SerializeField] private Material _material;
    [SerializeField] private TMP_Text _hpText;
    
    public event Action OnPlayerDeath;
    public event Action<float> OnTakeDamage;

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
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            OnPlayerDeath?.Invoke();
            PlayerController.IsPlaying = false;
        }
    }
}
