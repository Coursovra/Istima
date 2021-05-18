using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Управление атакой игрока
/// </summary>
public class PlayerAttackController : MonoBehaviour
{
    #region fields
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }
    public float AttackSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = (int) value;
    }
    public Vector3 ProjectileSize
    {
        set => _projectilePrefab.transform.localScale = value;
    }

    public float ProjectileSpeed
    {
        set => _projectilePrefab.GetComponent<ProjectileView>().Speed = value;
    }
    
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileParentTransorm;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private PlayerSpriteController _playerSpriteController;
    private List<GameObject> _projectilePrefabs = new List<GameObject>();
    private int _damage;
    private float _attackSpeed;
    private float _lastAttackTimer;
    private GameObject[] _projectileSpawnPoints;
    #endregion

    /// <summary>
    /// Получаем характеристика скина игрока при старте
    /// Подпись на событие выбора нового скина из магазига.
    /// Подпись на событие попадания выстрелом по препятствию.
    /// </summary>
    private void Start()
    {
        GetAttackStats();
        ShopItemInfoPanelView.OnSelectButtonClicked += ShopItemInfoPanelViewOnSelectButtonClicked;
        ProjectileView.OnHit += ProjectileViewOnHit;
    }

    /// <summary>
    /// При ввыборе нового скина получаем его характеристики
    /// </summary>
    /// <param name="obj">Скин</param>
    private void ShopItemInfoPanelViewOnSelectButtonClicked(SkinButtonView obj)
    {
        GetAttackStats();
    }

    /// <summary>
    /// Получаем урон и скорость атаки скина, а также его позицию для спавна проджектайлов (выстрелов)
    /// </summary>
    public void GetAttackStats()
    {
        var selectedSkin = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
        _damage = (int)Math.Round(selectedSkin.Damage);
        _attackSpeed = selectedSkin.AttackSpeed;
        _projectileSpawnPoints = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>().GetProjectileSpawnPoints();
    }

    /// <summary>
    /// Отписка от событий при уничтожении объекта игрока
    /// </summary>
    private void OnDestroy()
    {
        ProjectileView.OnHit -= ProjectileViewOnHit;
        ShopItemInfoPanelView.OnSelectButtonClicked -= ShopItemInfoPanelViewOnSelectButtonClicked;
    }

    /// <summary>
    /// Если игрок не играет, уничтожение всех выстрелов 
    /// </summary>
    private void Update()
    {
        _lastAttackTimer += Time.deltaTime;
        if (!PlayerController.IsPlaying) //todo: можно убрать?
        {
            foreach (var projectileView in _projectilePrefabs)
            {
                Destroy(projectileView);
            }
            _projectilePrefabs.Clear();
        }
    }
    
    /// <summary>
    /// Если игрок играет (летит), происходит создание проджектайла (выстрела) с частотой, равной скорости атаки
    /// </summary>
    private void FixedUpdate()
    {
        if(!PlayerController.IsPlaying) { return; }
        
        if (!(_lastAttackTimer >=  1 / AttackSpeed)) return;
        
        SpawnProjectile();
        _lastAttackTimer = 0;
    }

    /// <summary>
    /// Создание выстрела на позиции, которую получаем из префаба скина
    /// </summary>
    private void SpawnProjectile()
    {
        foreach (var spawnPoint in _projectileSpawnPoints)
        {
            Vector2 position = new Vector2(spawnPoint.transform.position.x,
                _playerSpriteController.transform.position.y);
            
            var projectileInstance = Instantiate(_projectilePrefab, position, Quaternion.identity,
                _projectileParentTransorm);
            
            _projectilePrefabs.Add(projectileInstance);
        }
    }
    
    /// <summary>
    /// Обработка попадания по препятствию.
    /// Нанесение урона, получение очков.
    /// </summary>
    /// <param name="obstacleView">Препятствие, по которому попал игрок</param>
    private void ProjectileViewOnHit(ObstacleView obstacleView)
    {
        obstacleView.SetHitPoints((int) (obstacleView.HitPoints - Damage));
        _scoreView.CurrentScore += (int) Math.Round(obstacleView.ScoreForHit * _damage * .1f, MidpointRounding.AwayFromZero); //todo: balance??
    }
}
