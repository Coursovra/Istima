using TMPro;
using UnityEngine;

public class UpgradePanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _attackSpeedText;
    [SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject;
    [SerializeField] private ScoreView _scoreView;
    private int _upgradeDamageCost= 1;
    private int _upgradeAttackSpeedCost= 1;
    private int _upgradeDamageValue = 1;
    private int _upgradeAttackSpeedValue = 1;

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _damageText.text = "Урон: " + _selectedSkinScriptableObject.SelectedSkin.Damage;
        _attackSpeedText.text = "Скорость атаки: " + _selectedSkinScriptableObject.SelectedSkin.AttackSpeed;
    }

    public void UpgradeDamage()
    {
        if (PlayerPrefs.GetInt("Score") >= _upgradeDamageCost)
        {
            _scoreView.Score -= _upgradeDamageCost;
            _selectedSkinScriptableObject.SelectedSkin.Damage += _upgradeDamageValue;
            UpdateText();
        }
    }
    
    public void UpgradeAttackSpeed()
    {
        if (PlayerPrefs.GetInt("Score") >= _upgradeAttackSpeedCost)
        {
            _scoreView.Score -= _upgradeAttackSpeedCost;
            _selectedSkinScriptableObject.SelectedSkin.AttackSpeed += _upgradeAttackSpeedValue;
            UpdateText();
        }
    }
}
