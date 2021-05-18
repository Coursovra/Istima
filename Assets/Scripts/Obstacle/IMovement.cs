using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Интерфейс для управления движением препятствий
/// </summary>
public interface IMovement
{
    float Speed { get; set; }
    
    void ProcessMovement();
}
