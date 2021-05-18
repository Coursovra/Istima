using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Интерфейс для управления созданием препятствий
/// </summary>
public interface IObstaclesSpawn 
{
    float LastSpawnTime { get; set; }
    
    List<GameObject> Obstacles { get; set; }
    
    public void Create();
}