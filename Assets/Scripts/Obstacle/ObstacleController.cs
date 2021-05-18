using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour, IMovement, IObstaclesSpawn, IDifficultHandler
{
	/// <summary>
	/// Класс для управлением препятствий
	/// </summary>
	public class ObstacleController : MonoBehaviour, IMovement, IObstaclesSpawn, IDifficultHandler
	{
	#region Fields
    [SerializeField] private ObstacleParentView _obstaclesParentView;
    [SerializeField] private UIController _uiController;
    [SerializeField] private PlayerAttackController _playerAttackController;
    public GameObject ObstaclePrefab;
    public float ScreenPercentage { get; set; } = .0055f; //todo: (pomoika) => optimize for diff screens
    public int ObstacleHitPoints { get; set; }
    public float SpawnRate { get; set; } = 4f;
    public float Speed { get; set; };
    public float LastSpawnTime { get; set; }
    public float ObstaclesQuantity { get; set; } = 3;
    public List<GameObject> Obstacles { get; set; }
  
    private float _gridRectHeight = 3f;
    public List<ObstacleParentView> ObstaclesParents = new List<ObstacleParentView>();
    private bool _isTriggered;
    private float _distance = 8.0f;
    private int _obstacleMinimumHp = 1;
    private int _obstacleMaximumHp = 1;
    private float _obstacleMinimumHpModifier;
    private float _obstacleMaximumHpModifier;
    private float _playerDps;
    private int _waveCounter;

	#endregion

    /// <summary>
    /// Отключение переменной при начале игры, которая позволяет обрабатывать смерть игрока.
    /// Нужна для того чтобы избежать двойных вызовов функций с обработкой смерти игрока
    /// </summary>
    private void OnEnable()
    {
        _isTriggered = false;
        DifficultReset();
    }
    
    private void DifficultReset()
    {
        _waveCounter = 1;
        _playerAttackController.GetAttackStats();
        _playerDps = _playerAttackController.Damage * _playerAttackController.AttackSpeed;
        _obstacleMinimumHpModifier = 1f;
        _obstacleMaximumHpModifier = 4f;
        _obstacleMinimumHp = (int) (_playerDps * _obstacleMinimumHpModifier);
        _obstacleMaximumHp = (int) (_playerDps * _obstacleMaximumHpModifier);
    }


    /// <summary>
    /// Вычисление точки спавна препятствий
    /// </summary>
    private void Start()
    {
        DifficultReset();
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
        if (Time.time > 1 / Time.time + SpawnRate + LastSpawnTime)
        {
            Create();
        }

        if (Obstacles.Count <= 0) return;
        ProcessMovement();
        DifficultHandler();
    }

    /// <summary>
    /// Создание препятствий, подписка каждого препятствия на обработку смерти игрока
    /// </summary>
    public void Create()
    {
        _waveCounter++;
        ObstaclesParents.Add(Instantiate(_obstaclesParentView, transform));
        for (int i = 0; i < ObstaclesQuantity; i++)
        {
            ObstacleHitPoints = Random.Range(_obstacleMinimumHp, _obstacleMaximumHp);
            var newObstacle = Instantiate(ObstaclePrefab, ObstaclesParents[ObstaclesParents.Count-1].transform);
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
    public void DifficultHandler() //todo: balance, max 6 wave
    {
        #region HP
        _obstacleMinimumHpModifier = Mathf.Clamp(_waveCounter, 1, 10);
        _obstacleMaximumHpModifier = Mathf.Clamp(_waveCounter, 4, 20);
        _obstacleMinimumHp = (int) (_playerDps * _obstacleMinimumHpModifier);
        _obstacleMaximumHp = (int) (_playerDps * _obstacleMaximumHpModifier);
        #endregion

        #region Quantity

        //ObstaclesQuantity = Mathf.Clamp(_waveCounter, 3, 9);

        //ObstaclesParents[ObstaclesParents.Count-1].GridLayoutGroup.cellSize = new Vector2(ObstaclesQuantity, ObstaclesQuantity);

        #endregion

        #region Speed

        Speed = Mathf.Clamp(_waveCounter, 4.5f, 9);

        #endregion

        #region SpawnRate

        //SpawnRate = Mathf.Clamp(_waveCounter, 4, 2);
        
        #endregion

        print($"wave: {_waveCounter} \n" +
              $"HP\n" +
              $" _obstacleMinimumHpModifier: {_obstacleMinimumHpModifier}, _obstacleMaximumHpModifier: {_obstacleMaximumHpModifier}\n" +
              $"_obstacleMinimumHp: {_obstacleMinimumHp}, _obstacleMaximumHp: {_obstacleMaximumHp}\n" +
              $"Quantity: {ObstaclesQuantity}\n" +
              $"Speed: {Speed}\n" +
              $"SpawnRate: {SpawnRate}");

        //todo: process hp, amount, grid.cell size... (formula?)
    }
    
    /// <summary>
    /// Движение каждого препятствия вниз по y-оси
    /// </summary>
    public void ProcessMovement()
    {
        foreach (var obstaclesParent in ObstaclesParents)
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
        
        foreach (var obstaclesParent in ObstaclesParents)
        {
            Destroy(obstaclesParent.gameObject);
        }
        Obstacles.Clear();
        ObstaclesParents.Clear();
    }
    
    /// <summary>
    /// Обработка смерти игрока
    /// </summary>
    private void ObstacleViewOnPlayerDeath()
    {
        if(_isTriggered) { return; }
        DestroyObstacles();
        _isTriggered = true;
        _uiController.ToggleUi(false);
    }
}
