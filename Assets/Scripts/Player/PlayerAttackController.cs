using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    private const float ScorePerAttackModifier = .04f;
    [SerializeField] private List<GameObject> _projectileSpawnPoints;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileParentTransorm;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private PlayerSpriteController _playerSpriteController;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _attackSfx;
    private List<GameObject> _projectilePrefabs = new List<GameObject>();
    private int _damage;
    private int _damageBoost;
    private float _attackSpeed;
    private float _attackSpeedBoost;
    private float _lastAttackTimer;
    private SkinView _selectedSkin;
    #endregion

    public ProjectileView GetProjectileView()
    {
        return _projectilePrefab.GetComponent<ProjectileView>();
    }
    public void SetDamageBoost(float damageBoost)
    {
        _damageBoost += (int) (_damage * damageBoost);
    }
    public void SetAttackSpeedBoost(float attackSpeedBoost)
    {
        _attackSpeedBoost += _attackSpeed * attackSpeedBoost;
    }

    public void SetAttackSfx(AudioClip audioClip)
    {
        _attackSfx = audioClip;
    }

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

    public SkinView GetSelectedSkin()
    {
        return _selectedSkin;
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
        _selectedSkin = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
        _damage = (int)Math.Round(_selectedSkin.Damage);
        _attackSpeed = _selectedSkin.AttackSpeed;
        _projectileSpawnPoints = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>().GetProjectileSpawnPoints();
    }

    public List<GameObject> GetSpawnPoints()
    {
        return _projectileSpawnPoints;
    }
    public List<GameObject> GetAdditionalSpawnPoints()
    {
        return _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>().GetAdditionalProjectileSpawnPoints();;
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
        if (!PlayerController.IsPlaying)
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
        
        if (!(_lastAttackTimer >=  1 / (AttackSpeed + _attackSpeedBoost))) return;
        
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
            
            var projectileInstance = Instantiate(_projectilePrefab, position, spawnPoint.transform.localRotation,
                _projectileParentTransorm);
            
            _projectilePrefabs.Add(projectileInstance);
            
            _audioSource.PlayOneShot(_attackSfx);
        }
    }
    
    /// <summary>
    /// Обработка попадания по препятствию.
    /// Нанесение урона, получение очков.
    /// </summary>
    /// <param name="obstacleView">Препятствие, по которому попал игрок</param>
    private void ProjectileViewOnHit(ObstacleView obstacleView)
    {
        obstacleView.SetHitPoints((int) (obstacleView.HitPoints - (Damage + _damageBoost)));
        var scoreToAdd = (int) Math.Round(
            obstacleView.ScoreForHit * (Damage + _damageBoost) * ScorePerAttackModifier,
            MidpointRounding.AwayFromZero);
        if (scoreToAdd == 0)
        {
            scoreToAdd++;
        }
        _scoreView.CurrentScore += scoreToAdd;
    }
}
