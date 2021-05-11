using System;
using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    #region fields
    
    public static event Action<ObstacleView> OnHit;
    public float Speed
    {
        set => _speed = value;
    }
    [SerializeField] private float _speed;
    
    #endregion

    private void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnHit?.Invoke(other.GetComponent<ObstacleView>());
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
