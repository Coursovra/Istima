using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вью скина с его характеристиками
/// </summary>
public class SkinView : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    [SerializeField] private List<GameObject> _projectileSpawnPoints;
    [SerializeField] private List<GameObject> _additionalProjectileSpawnPoints;
    [SerializeField] public float Price;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public float Damage;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public int Id;
  
    public List<GameObject> GetProjectileSpawnPoints()
    {
        return _projectileSpawnPoints;
    }
    
    public List<GameObject> GetAdditionalProjectileSpawnPoints()
    {
        return _additionalProjectileSpawnPoints;
    }

    public void SwitchAdditionalSpawnPoints(bool value)
    {
        foreach (var additionalProjectileSpawnPoint in _additionalProjectileSpawnPoints)
        {
            additionalProjectileSpawnPoint.SetActive(value);
        }
    }
}
