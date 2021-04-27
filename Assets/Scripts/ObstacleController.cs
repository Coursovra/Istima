using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour, IMovement, IObstaclesSpawn, IDifficultHandler
{
    [SerializeField] private GameObject _obstaclesParentPrefab;
    [SerializeField] private UIController _uiController;
    public GameObject ObstaclePrefab;
    public float Speed { get; set; } = .015f;
    public float ScreenPercentage { get; set; } = .0055f; //todo: (pomoika) => optimize for diff screens
    public float ObstacleHitPoints { get; set; }
    public float SpawnRate { get; set; } = 5f;
    public float LastSpawnTime { get; set; }
    public float ObstaclesQuantity { get; set; } = 3;
    public List<GameObject> Obstacles { get; set; }
    
    private float _gridRectHeight = 3f;
    private GameObject _obstaclesParent;

    private void Start()
    {
        Obstacles = new List<GameObject>();
        transform.position += new Vector3(0, Screen.height * ScreenPercentage, 0);
        Create();
    }

    private void Update()
    {
        DifficultHandler();

        if (Time.time > 1 / Time.time + SpawnRate + LastSpawnTime)
        {
            Create();
        }

        if (Obstacles.Count > 0)
        {
            ProcessMovement();
        }
    }

    public void Create()
    {
        Obstacles.Clear();
        _obstaclesParent = Instantiate(_obstaclesParentPrefab, transform);
        for (int i = 0; i < ObstaclesQuantity; i++)
        {
            ObstacleHitPoints = Random.Range(30, 200); //todo: move to Difficult
            var newObstacle = Instantiate(ObstaclePrefab, _obstaclesParent.transform);
            var obstacleView = newObstacle.GetComponent<ObstacleView>();
            obstacleView.SetHitPoints(ObstacleHitPoints);
            obstacleView.OnPlayerDeath += ObstacleViewOnPlayerDeath; //todo: сраюатывает несколько раз (если затриггерить 2 блока)
            Obstacles.Add(newObstacle);
        }
        LastSpawnTime = Time.time;
    }

    private void ObstacleViewOnPlayerDeath()
    {
        DestroyObstacles();
        print("you lose\n)))");
        _uiController.SwitchUi(false);
    }

    public void DifficultHandler()
    {
        //todo: process hp, amount, grid.cell size... (formula?)
    }
    
    public void ProcessMovement()
    {
        _obstaclesParent.transform.position -= new Vector3(0, Speed, 0);
        
        if (Obstacles[0].transform.position.y <= -Screen.height * ScreenPercentage)
        {
            DestroyObstacles();
        }
    }

    private void DestroyObstacles()
    {
        foreach (var obstacle in Obstacles)
        {
            obstacle.GetComponent<ObstacleView>().OnPlayerDeath -= ObstacleViewOnPlayerDeath;
        }

        Destroy(_obstaclesParent);
        Obstacles.Clear();
    }
}
