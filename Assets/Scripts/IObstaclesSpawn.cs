using System.Collections.Generic;
using UnityEngine;

public interface IObstaclesSpawn
{
    float LastSpawnTime { get; set; }
    
    List<GameObject> Obstacles { get; set; }
    
    //public void Spawn();
    

    public void Create();
}