using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    float Speed { get; set; }
    
    float ScreenPercentage { get; set; }

    void ProcessMovement();
}
