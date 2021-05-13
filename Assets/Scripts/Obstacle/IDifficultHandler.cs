using UnityEngine;

/// <summary>
/// Интерфейс для управления количества препятствий, их ХП, частота спавна
/// </summary>
public interface IDifficultHandler
{
    float ObstaclesQuantity { get; set; } 
    
    float ObstacleHitPoints { get; set; }
    
    float SpawnRate { get; set; }

    public void DifficultHandler();
}
