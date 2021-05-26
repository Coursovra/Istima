using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoostsController : MonoBehaviour
{
    [SerializeField] private GameObject[] _availableBoosts;
    [SerializeField] private Image _boostIcon;
    [SerializeField] private Image _boostStatus;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private PlayerAttackController _playerAttackController;
    private List<IBoost> _activeBoosts = new List<IBoost>();
    private List<GameObject> _spawnedBoostPrefabs = new List<GameObject>();
    private float _timer;
    private float _chanceToSpawn = .1f;
    private float _spawnRate = 1f;
    private float _lastSpawnWave;
    private float _spawnYPosition;
    private float _spawnXPosition;
    private float xOffset = .3f;
    private float _spawnYOffset = 1f;

    private void Start()
    {
        ObstacleView.OnPlayerDeath += ObstacleViewOnPlayerDeath;
        var rectTransform = _obstacleController.ObstaclePrefab.GetComponent<RectTransform>();
        var height = rectTransform.rect.height;
        _spawnYOffset = (height + height) * _obstacleController.ObstaclePrefab.transform.localScale.y;
        _spawnYPosition = _obstacleController.transform.position.y + _spawnYOffset;
    }

    private void OnEnable()
    {
        _lastSpawnWave = _obstacleController.GetCurrentWave();
    }

    private void ObstacleViewOnPlayerDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _boostIcon.gameObject.SetActive(false);
        _boostStatus.gameObject.SetActive(false);
        foreach (var activeBoost in _activeBoosts)
        {
            activeBoost.OnTimeIsUp -= OnTimeIsUp;
            activeBoost.OnPickedUp -= OnPickedUp;
            activeBoost.DisableEffect(_playerAttackController);
        }

        foreach (var spawnedBoostPrefab in _spawnedBoostPrefabs)
        {
            Destroy(spawnedBoostPrefab);
        }
        
        _activeBoosts.Clear();
        _spawnedBoostPrefabs.Clear();

        _spawnRate = 1;
        _chanceToSpawn = .1f;
    }

    private void SpawnHandler()
    {
        if (Random.value <= _chanceToSpawn)
        {
            var worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
            _spawnXPosition = Random.Range(-worldDimensions.x + worldDimensions.x * xOffset, worldDimensions.x - worldDimensions.x * xOffset);
            var spawnPosition = new Vector2(_spawnXPosition, _spawnYPosition);
            var boostPrefab = Instantiate(_availableBoosts[Random.Range(0, _availableBoosts.Length)], spawnPosition, Quaternion.identity, transform);
            var boost = boostPrefab.GetComponent<IBoost>();
            boost.OnPickedUp += OnPickedUp;
            boost.OnTimeIsUp += OnTimeIsUp;
            boost.OnInvisible += OnInvisible;
            boost.Prefab = boostPrefab;
            _spawnedBoostPrefabs.Add(boostPrefab);
            _chanceToSpawn = .1f;
            _spawnRate = Random.Range(2, 5);
        }
        else
        {
            _chanceToSpawn += .2f;
        }
    }


    private void Update()
    {
        if (_obstacleController.GetCurrentWave() > _lastSpawnWave + _spawnRate)
        {
            SpawnHandler();
            _lastSpawnWave = _obstacleController.GetCurrentWave();
        }
        
        if (_spawnedBoostPrefabs.Count == 0) { return; }

        foreach (var boostPrefab in _spawnedBoostPrefabs)
        {
            boostPrefab.transform.position -= new Vector3(0, _obstacleController.Speed * Time.deltaTime, 0);
        }

        if (_activeBoosts.Count == 0)
        {
            _boostIcon.gameObject.SetActive(false);
            _boostStatus.gameObject.SetActive(false);
            return;
        }
        
        _boostIcon.gameObject.SetActive(true);
        _boostStatus.gameObject.SetActive(true);

        _boostStatus.color = _activeBoosts[0].BoostStatusBarColor;
        _timer += Time.deltaTime;
        var percent = _timer / _activeBoosts[0].Duration;
        _boostStatus.fillAmount = 1 - Mathf.Lerp(0, 1, percent);
    }

    private void OnInvisible(IBoost boost)
    {
        boost.OnTimeIsUp -= OnTimeIsUp;
        boost.OnPickedUp -= OnPickedUp;
        _spawnedBoostPrefabs.Remove(boost.Prefab);
        Destroy(boost.Prefab);
    }

    private void OnTimeIsUp(IBoost boost)
    {
        Destroy(boost.Prefab);
        boost.OnTimeIsUp -= OnTimeIsUp;
        boost.OnPickedUp -= OnPickedUp;
        _spawnedBoostPrefabs.Remove(boost.Prefab);
        boost.DisableEffect(_playerAttackController);
        _activeBoosts.Remove(boost);
    }

    private void OnPickedUp(IBoost boost)
    {
        if(boost.Activated) { return; }
        _activeBoosts.Add(boost);
        _boostIcon.sprite = _activeBoosts[0].Icon;
        boost.EnableEffect(_playerAttackController);
        _timer = 0;
        boost.CurrentGameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
