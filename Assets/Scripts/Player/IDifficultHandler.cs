using UnityEngine;

public interface IDifficultHandler
{
    float ObstaclesQuantity { get; set; } 
    
    float ObstacleHitPoints { get; set; }
    
    float SpawnRate { get; set; }

    public void DifficultHandler();
}
