using System;
using UnityEngine;

public class TripleFiringBoost : MonoBehaviour, IBoost
{
    public Sprite Icon => _icon;

    public Color BoostStatusBarColor => _boostStatusBarColor;

    public float Duration => _duration;

    public float DamagePercent { get; set; }

    public float AttackSpeedPercent { get; set; }

    public GameObject CurrentGameObject => gameObject;

    public GameObject Prefab
    {
        get => _prefab;
        set => _prefab = value;
    }

    public Action<IBoost> OnTimeIsUp { get; set; }
    public Action<IBoost> OnPickedUp { get; set; }
    public Action<IBoost> OnInvisible { get; set; }
    
    public bool Activated { get; set; }
    
    [SerializeField] private float _duration;
    [SerializeField] private Color _boostStatusBarColor;
    [SerializeField] private Sprite _icon;

    private float _timer;
    private GameObject _prefab;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _timer = _duration;
    }
    
    public void EnableEffect(PlayerAttackController playerAttackController)
    {
        playerAttackController.GetSpawnPoints().AddRange(playerAttackController.GetAdditionalSpawnPoints());
        playerAttackController.GetSelectedSkin().SwitchAdditionalSpawnPoints(true);
    }

    public void DisableEffect(PlayerAttackController playerAttackController)
    {
        foreach (var additionalSpawnPoint in playerAttackController.GetAdditionalSpawnPoints())
        {
            playerAttackController.GetSpawnPoints().Remove(additionalSpawnPoint);
        }
        playerAttackController.GetSelectedSkin().SwitchAdditionalSpawnPoints(false);
    }
    
    private void Update()
    {
        var distance = -8.0f;
        var frustumHeight = 2.0f * distance * Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        if (!Activated) { return; }
        
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            OnTimeIsUp?.Invoke(this);
        }

        if (transform.position.y < frustumHeight)
        {
            if(Activated) { return; }
            OnInvisible?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent<PlayerAttackController>(out var component))
        {
            OnPickedUp?.Invoke(this);
            Activated = true;
        }
    }
}
