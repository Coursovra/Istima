using System;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        GetAttackStats();
        ShopItemInfoPanelView.OnSelectButtonClicked += ShopItemInfoPanelViewOnSelectButtonClicked;
        ProjectileView.OnHit += ProjectileViewOnHit;
    }

    private void ShopItemInfoPanelViewOnSelectButtonClicked(SkinButtonView obj)
    {
        GetAttackStats();
    }

    public void GetAttackStats()
    {
        var selectedSkin = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
        _damage = (int)Math.Round(selectedSkin.Damage);
        _attackSpeed = selectedSkin.AttackSpeed;
        _projectileSpawnPoints = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>().GetProjectileSpawnPoints();
    }

    private void OnDestroy()
    {
        ProjectileView.OnHit -= ProjectileViewOnHit;
        ShopItemInfoPanelView.OnSelectButtonClicked -= ShopItemInfoPanelViewOnSelectButtonClicked;

    }

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

    private void FixedUpdate()
    {
        if(!PlayerController.IsPlaying) { return; }
        
        if (!(_lastAttackTimer >=  1 / AttackSpeed)) return;
        
        SpawnProjectile();
        _lastAttackTimer = 0;
    }

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
    
    private void ProjectileViewOnHit(ObstacleView obstacleView)
    {
        obstacleView.SetHitPoints((int) (obstacleView.HitPoints - Damage));
        _scoreView.CurrentScore += (int) Math.Round(obstacleView.ScoreForHit * _damage * .1f, MidpointRounding.AwayFromZero); //todo: balance??
    }
}
