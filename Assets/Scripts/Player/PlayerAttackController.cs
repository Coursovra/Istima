using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    #region fields
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    public float AttackSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = value;
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
    [SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject;
    [SerializeField] private Transform _projectileParentTransorm;
    [SerializeField] private ScoreView _scoreView;
    private List<GameObject> _projectilePrefabs = new List<GameObject>();
    private float _damage;
    private float _attackSpeed;
    private float _lastAttackTimer;
    private GameObject[] _projectileSpawnPoints;
    private PlayerSpriteController _playerSpriteController;
    #endregion

    private void Start()
    {
        _playerSpriteController = GetComponentInChildren<PlayerSpriteController>();
        var selectedSkin = _selectedSkinScriptableObject.SelectedSkin;
        _damage = selectedSkin.Damage;
        _attackSpeed = selectedSkin.AttackSpeed;
         _projectileSpawnPoints = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>()
             .GetProjectileSpawnPoints();

        print(_playerSpriteController.GetPlayerSkinInstance());
        ProjectileView.OnHit += ProjectileViewOnHit;
        
    }

    private void OnDestroy()
    {
        ProjectileView.OnHit -= ProjectileViewOnHit;
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
        
        if (!(_lastAttackTimer >= 1 / AttackSpeed)) return;
        
        SpawnProjectile();
        _lastAttackTimer = 0;
    }

    public void SpawnProjectile()
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
        obstacleView.SetHitPoints(obstacleView.HitPoints - Damage);
        _scoreView.CurrentScore += obstacleView.ScoreForHit;
    }
}
