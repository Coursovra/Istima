using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс объекта, в котором создаются препятствия
/// </summary>
public class ObstacleParentView : MonoBehaviour
{
    [SerializeField] public GridLayoutGroup GridLayoutGroup;
    [SerializeField] private ObstacleController _obstacleController;
    private Camera _camera;

    private void Start()
    {
        _obstacleController = FindObjectOfType<ObstacleController>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if(_camera == null) { return; }
        
        var distance = -8.0f;
        var frustumHeight = 2.0f * distance * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        if (!(transform.position.y < frustumHeight)) return;
        
        _obstacleController.ObstaclesParents.Remove(this);
        Destroy(gameObject);
    }
}
