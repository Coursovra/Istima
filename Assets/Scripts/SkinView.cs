using UnityEngine;

public class SkinView : MonoBehaviour
{
    [SerializeField] private GameObject[] _projectileSpawnPoints;

    public GameObject[] GetProjectileSpawnPoints()
    {
        return _projectileSpawnPoints;
    }
}
