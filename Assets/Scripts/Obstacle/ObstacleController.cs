using System.Collections.Generic;
using UnityEngine;

	/// <summary>
	/// Класс для управлением препятствий
	/// </summary>
	public class ObstacleController : MonoBehaviour, IMovement, IObstaclesSpawn, IDifficultHandler
	{
	#region Fields
    [SerializeField] private GameObject _obstaclesParentPrefab;
    [SerializeField] private GameObject _slider;
    [SerializeField] private UIController _uiController;
    [SerializeField] private PlayerController _playerController;
    public GameObject ObstaclePrefab;
    public float Speed { get; set; } = 5;
    public float ObstacleHitPoints { get; set; }
    public float SpawnRate { get; set; } = 5.5f;
    public float LastSpawnTime { get; set; }
    public float ObstaclesQuantity { get; set; } = 3;
    public List<GameObject> Obstacles { get; set; }
    
    private float _gridRectHeight = 3f;
    private List<GameObject> _obstaclesParents = new List<GameObject>();
    private bool _isTriggered;
    private float _distance = 8.0f;
	#endregion

    /// <summary>
    /// Отключение переменной при начале игры, которая позволяет обрабатывать смерть игрока.
    /// Нужна для того чтобы избежать двойных вызовов функций с обработкой смерти игрока
    /// </summary>
    private void OnEnable()
    {
        _isTriggered = false;
    }

    /// <summary>
    /// Вычисление точки спавна препятствий
    /// </summary>
    private void Start()
    {
        var frustumHeight = 2.0f * _distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        Obstacles = new List<GameObject>();
        transform.position += new Vector3(0, frustumHeight, 0);
        Create();
    }
    
    /// <summary>
    /// Управление сложностью, создание препятствий и их движние
    /// </summary>
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

    /// <summary>
    /// Создание препятствий, подписка каждого препятствия на обработку смерти игрока
    /// </summary>
    public void Create()
    {
        _obstaclesParents.Add(Instantiate(_obstaclesParentPrefab, transform));
        for (int i = 0; i < ObstaclesQuantity; i++)
        {
            ObstacleHitPoints = Random.Range(30, 200); //todo: move to Difficult
            var newObstacle = Instantiate(ObstaclePrefab, _obstaclesParents[_obstaclesParents.Count-1].transform);
            var obstacleView = newObstacle.GetComponent<ObstacleView>();
            obstacleView.SetHitPoints(ObstacleHitPoints);
            obstacleView.OnPlayerDeath += ObstacleViewOnPlayerDeath;
            Obstacles.Add(newObstacle);
        }
        LastSpawnTime = Time.time;
    }

    /// <summary>
    /// Управление сложностью игры.
    /// Количество препятствий, их хп, скорость движения
    /// </summary>
    public void DifficultHandler()
    {
        //todo: process hp, amount, grid.cell size... (formula?)
    }
    
    /// <summary>
    /// Движение каждого препятствия вниз по y-оси
    /// </summary>
    public void ProcessMovement()
    {
        foreach (var obstaclesParent in _obstaclesParents)
        {
            obstaclesParent.transform.position -= new Vector3(0, Speed * Time.deltaTime, 0);
        }
        
        // if (Obstacles[0].transform.position.y <= -Screen.height * ScreenPercentage)
        // {
        //     DestroyObstacles();
        // }
    }

    /// <summary>
    /// Уничтожение всех препятствий, отписка от события, очищение листов
    /// </summary>
    private void DestroyObstacles()
    {
        _isTriggered = false;
        foreach (var obstacle in Obstacles)
        {
            obstacle.GetComponent<ObstacleView>().OnPlayerDeath -= ObstacleViewOnPlayerDeath;
        }

        foreach (var obstaclesParent in _obstaclesParents)
        {
            Destroy(obstaclesParent);
        }
        Obstacles.Clear();
        _obstaclesParents.Clear();
    }
    
    /// <summary>
    /// Обработка смерти игрока
    /// </summary>
    private void ObstacleViewOnPlayerDeath()
    {
        if(_isTriggered) { return; }
        DestroyObstacles();
        _isTriggered = true;
        SwitchUi();
    }
    
    /// <summary>
    /// Отоброжение интерфейса
    /// </summary>
    private void SwitchUi()
    {
        _slider.SetActive(false);
        gameObject.SetActive(false);
        _uiController.Menu.SetActive(true);
        //_uiController.StartGameObjects.SetActive(true);
        _uiController.DeathScreen.SetActive(true);
        _uiController.UpgradePanel.SetActive(true);
        _uiController.ShopButton.SetActive(true);
    }
}
