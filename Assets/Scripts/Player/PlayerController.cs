using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool IsPlaying;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private BoostsController _boostsController;
    [SerializeField] private PlayerSpriteController _playerSpriteController;
    [SerializeField] private PlayerAttackController _playerAttackController;
    [SerializeField] private UpgradePanelView _upgradePanelView;
    [SerializeField] private ScoreView _scoreView;
    private readonly Vector2 _startPosition = new Vector2(0, -8);
    /// <summary>
    /// Если это первый запуск игры, пишем в PlayerPrefs характеристики стандартного скина
    /// Иначе получаем из PlayerPrefs характеристики текущего скина
    /// </summary>
    private void Start()
    {
        if (PlayerPrefsController.GetMySkins().Length == 0)
        {
            WriteDefault();
        }
        else
        {
            var currentSkin = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
            var mySkins = PlayerPrefsController.GetMySkins();
            foreach (var newString in mySkins.Split(';'))
            {
                if(newString.Length == 0) { continue; }
                var array = newString.Split('-');
                var id = array[0];
                var damage = array[1];
                var attackSpeed = array[2];
                
                if (id != currentSkin.Id.ToString()) continue;
                currentSkin.Damage = Convert.ToSingle(damage);
                currentSkin.AttackSpeed = Convert.ToSingle(attackSpeed);
            }
        }
        _upgradePanelView.UpdateText();
    }

    /// <summary>
    /// Запись в PlayerPrefs
    /// </summary>
    private void WriteDefault()
    {
        var selectedSkinView = _playerSpriteController.GetSelectedSkinView();
        PlayerPrefsController.SetScore(0);
        PlayerPrefsController.SetMySkins(
            $"{selectedSkinView.Id}-{selectedSkinView.Damage}-{selectedSkinView.AttackSpeed}");
        // PlayerPrefsController.SetUpgradeDamageCost(1);
        // PlayerPrefsController.SetUpgradeDamageValue(1);
        // PlayerPrefsController.SetUpgradeAttackSpeedCost(1);
        // PlayerPrefsController.SetUpgradeAttackSpeedValue(1);
    }

    /// <summary>
    /// При нажатии на игрока начинается игра
    /// </summary>
    private void OnMouseDown()
    {
        if (IsPlaying) return;

        StartGame();
    }

    private void StartGame()
    {
        ResetPosition();
        _uiController.ToggleUi(true);
        _obstacleController.gameObject.SetActive(true);
        _boostsController.gameObject.SetActive(true);

        IsPlaying = true;
        _scoreView.CurrentScore = 0;
        _playerAttackController.GetAttackStats();
    }

    /// <summary>
    /// Установка игрока в стартовую точку
    /// </summary>
    public void ResetPosition()
    {
        transform.position = _startPosition;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.TryGetComponent<IBoost>(out var boost))
    //     {
    //         _playerAttackController.ActiveBoosts.Add(boost);
    //     }
    // }
}
