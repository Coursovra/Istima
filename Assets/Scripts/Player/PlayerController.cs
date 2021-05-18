using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool IsPlaying;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private PlayerSpriteController _playerSpriteController;
    [SerializeField] private PlayerAttackController _playerAttackController;
    [SerializeField] private UpgradePanelView _upgradePanelView;
    [SerializeField] private ScoreView _scoreView;
    private readonly Vector2 _startPosition = new Vector2(0, -8);

    
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

    private void WriteDefault()
    {
        var selectedSkinView = _playerSpriteController.GetSelectedSkinView();
        PlayerPrefsController.SetScore(0);
        PlayerPrefsController.SetMySkins(
            $"{selectedSkinView.Id}-{selectedSkinView.Damage}-{selectedSkinView.AttackSpeed}");
        PlayerPrefsController.SetUpgradeDamageCost(1);
        PlayerPrefsController.SetUpgradeDamageValue(1);
        PlayerPrefsController.SetUpgradeAttackSpeedCost(1);
        PlayerPrefsController.SetUpgradeAttackSpeedValue(1);
    }

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

        IsPlaying = true;
        _scoreView.CurrentScore = 0;
        _playerAttackController.GetAttackStats();
    }

    private void ResetPosition()
    {
        transform.position = _startPosition;
    }
    
}
