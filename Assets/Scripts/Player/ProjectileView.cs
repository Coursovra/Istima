using System;
using UnityEngine;

/// <summary>
/// Вью каждого выстрела
/// </summary>
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

    /// <summary>
    /// Движение объекта вверх по y-оси
    /// </summary>
    private void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * _speed);
    }

    /// <summary>
    /// Обработка попадания по препятствию, вызов события
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnHit?.Invoke(other.GetComponent<ObstacleView>());
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Уничтожение выстрела, который игрок уже не видит
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
