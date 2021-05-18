using UnityEngine;

public interface IDifficultHandler
{
    float ObstaclesQuantity { get; set; } 
    
    int ObstacleHitPoints { get; set; }
    
    float SpawnRate { get; set; }

    public void DifficultHandler();
}
