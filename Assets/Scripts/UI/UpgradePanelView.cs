using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Улучшение характеристик игрока;
/// Цены и значения улучшений формируются в зависимости от текщих характеристик игрока
/// </summary>
public class UpgradePanelView : MonoBehaviour //todo: balance???
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _attackSpeedText;
    [SerializeField] private PlayerSpriteController _playerSpriteController;
    [SerializeField] private ScoreView _scoreView;
    private float _damagePriceModifier = 2.5f;
    private float _attackSpeedPriceModifier = 50f;
    private float _damageUpgradeModifier = .1f;
    private float _attackSpeedUpgradeModifier = .07f;
    private SkinView _selectedSkin;

    private int DamagePrice => (int)Math.Round(_selectedSkin.Damage * _damagePriceModifier, MidpointRounding.AwayFromZero);

    private int AttackSpeedPrice => (int)Math.Round(_selectedSkin.AttackSpeed * _attackSpeedPriceModifier, MidpointRounding.AwayFromZero);

    private float DamageUpgrade => (float) Math.Round(_selectedSkin.Damage * _damageUpgradeModifier, 1);

    private float AttackSpeedUpgrade => (float)Math.Round(_selectedSkin.AttackSpeed * _attackSpeedUpgradeModifier, 1);

    private void Start()
    {
        ShopItemInfoPanelView.OnSelectButtonClicked += ShopItemInfoPanelViewOnSelectButtonClicked;
        _selectedSkin = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
        //PlayerPrefsController.Clear();
    }

    private void OnDestroy()
    {
        ShopItemInfoPanelView.OnSelectButtonClicked -= ShopItemInfoPanelViewOnSelectButtonClicked;
    }

    private void ShopItemInfoPanelViewOnSelectButtonClicked(SkinButtonView obj)
    {
        _selectedSkin = _playerSpriteController.GetPlayerSkinInstance().GetComponent<SkinView>();
        UpdateText();
    }
    
    public void UpdateText()
    {
        _damageText.text = $"Урон:\n {_selectedSkin.Damage} (+{DamageUpgrade})\nОчков: {DamagePrice}";
        _attackSpeedText.text = $"Скорость атаки: {_selectedSkin.AttackSpeed} (+{AttackSpeedUpgrade})\nОчков: {AttackSpeedPrice}";
    }
    
    public void UpgradeDamage()
    {
        if (_scoreView.Score < DamagePrice) { return; }

        _scoreView.Score -= DamagePrice;
        _selectedSkin.Damage += DamageUpgrade;

        UpdatePlayerPrefs();
        UpdateText();
    }

    public void UpgradeAttackSpeed()
    {
        if (_scoreView.Score < AttackSpeedPrice) { return; }
        
        _scoreView.Score -= AttackSpeedPrice;
        _selectedSkin.AttackSpeed += AttackSpeedUpgrade;
        
        UpdatePlayerPrefs();
        UpdateText();
    }
    

    private void UpdatePlayerPrefs()
    {
        var newString = $"{_selectedSkin.Id}-{_selectedSkin.Damage}-{_selectedSkin.AttackSpeed}";

        string newMySkinsString = "";
        var mySkinsString = PlayerPrefsController.GetMySkins();
        var mySkinsArray = mySkinsString.Split(';');
        foreach (var skinString in mySkinsArray)
        {
            if(skinString.Length == 0) { continue; }
            var array = skinString.Split('-');
            var id = array[0];
            var damage = array[1];
            var attackSpeed = array[2];
            if (id == _selectedSkin.Id.ToString())
            {
                newMySkinsString += newString;
            }
            else
            {
                newMySkinsString += $"{id}-{damage}-{attackSpeed};";
            }
        }   
        PlayerPrefsController.SetMySkins(newMySkinsString);
    }
}
