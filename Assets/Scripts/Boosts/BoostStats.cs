using System;
using UnityEngine;

public class BoostStats : MonoBehaviour, IBoost
{
    public Sprite Icon => _icon;

    public Color BoostStatusBarColor => _boostStatusBarColor;

    public float Duration => _duration;

    public float DamagePercent
    {
        get => damagePercent;
        set => damagePercent = value;
    }

    public float AttackSpeedPercent
    {
        get => attackSpeedPercent;
        set => attackSpeedPercent = value;
    }

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
    [SerializeField] private float damagePercent;
    [SerializeField] private float attackSpeedPercent;
    [SerializeField] private Sprite _defaultProjectileSprite;
    [SerializeField] private Sprite _damageBoostProjectileSprite;
    [SerializeField] private AudioClip _defaultAttackSfx;
    [SerializeField] private AudioClip _damageBoostAttackSfx;
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
        if (damagePercent > 0)
        {
            //playerAttackController.SetAttackSfx(_damageBoostAttackSfx);
            playerAttackController.GetProjectileView().SetSprite(_damageBoostProjectileSprite);
        }
        playerAttackController.SetDamageBoost(damagePercent);
        playerAttackController.SetAttackSpeedBoost(attackSpeedPercent);
    }

    public void DisableEffect(PlayerAttackController playerAttackController)
    {
        if (damagePercent > 0)
        {
            //playerAttackController.SetAttackSfx(_defaultAttackSfx);
            playerAttackController.GetProjectileView().SetSprite(_defaultProjectileSprite);
        }
        playerAttackController.SetDamageBoost(-damagePercent);
        playerAttackController.SetAttackSpeedBoost(-attackSpeedPercent);
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
