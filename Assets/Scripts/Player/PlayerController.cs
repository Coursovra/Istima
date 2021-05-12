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
    [SerializeField] private GameObject _slider;
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
        
        ResetPosition();
        SwitchUi();
        _uiController.DeathScreen.SetActive(false);
        _uiController.UpgradePanel.SetActive(false);
        IsPlaying = true;
        _scoreView.CurrentScore = 0;
        _playerAttackController.GetAttackStats();
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }
    
    private void SwitchUi()
    {
        _slider.SetActive(true);
        _uiController.Menu.SetActive(false);
        _uiController.Ui.SetActive(true);
        _uiController.StartGameObjects.SetActive(false);
        _uiController.DeathScreen.SetActive(true);
        _uiController.UpgradePanel.SetActive(true);
        _obstacleController.gameObject.SetActive(true);
        _uiController.ShopButton.SetActive(false);
    }
}
