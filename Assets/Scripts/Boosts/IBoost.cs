using System;
using UnityEngine;

/// <summary>
/// Интерфейс для усилений
/// </summary>
public interface IBoost
{
    Sprite Icon { get; }
    Color BoostStatusBarColor { get; }
    float Duration { get; }
    float DamagePercent { get; set; }
    float AttackSpeedPercent { get; set; }
    GameObject CurrentGameObject { get; }
    GameObject Prefab { get; set; }
    bool Activated { get; set; }

    void EnableEffect(PlayerAttackController playerAttackController);
    void DisableEffect(PlayerAttackController playerAttackController);
    
    Action<IBoost> OnTimeIsUp { get; set; }
    Action<IBoost> OnPickedUp { get; set; }
    Action<IBoost> OnInvisible { get; set; }
}
