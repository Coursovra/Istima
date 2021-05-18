using UnityEngine;

public class SkinView : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    [SerializeField] private GameObject[] _projectileSpawnPoints;
    [SerializeField] public float Price;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public float Damage;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public int Id;
    
    public GameObject[] GetProjectileSpawnPoints()
    {
        return _projectileSpawnPoints;
    }
}
