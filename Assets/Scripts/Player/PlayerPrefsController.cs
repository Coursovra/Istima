using UnityEngine;

public static class PlayerPrefsController
{
    private const string MySkinsStringPattern = "{Id}-{Damage}-{AttackSpeed};";
    
    #region Set

    public static void SetSelectedSkinId(int id)
    {
        PlayerPrefs.SetInt("SelectedSkinId", id);
    }

    public static void SetMySkins(string value)
    {
        PlayerPrefs.SetString("MySkins", value);
    }
    
    public static void SetScore(int value)
    {
        PlayerPrefs.SetInt("Score", value);
    }
    
    public static void SetBestScore(int value)
    {
        PlayerPrefs.SetInt("BestScore", value);
    }

    #region Upgrades

    public static void SetUpgradeDamageCost(int value)
    {
        PlayerPrefs.SetInt("UpgradeDamageCost", value);
    }
    
    public static void SetUpgradeDamageValue(int value)
    {
        PlayerPrefs.SetInt("UpgradeDamageValue", value);
    }
    
    public static void SetUpgradeAttackSpeedCost(int value)
    {
        PlayerPrefs.SetInt("UpgradeAttackSpeedCost", value);
    }
    
    public static void SetUpgradeAttackSpeedValue(float value)
    {
        PlayerPrefs.SetFloat("UpgradeAttackSpeedValue", value);
    }
    
    public static void SetFpsToggleStatus(int value)
    {
        PlayerPrefs.SetInt("SettingsFPSToggle", value);
    }

    #endregion
    

    #endregion

    #region Get

    public static string GetMySkins()
    {
        return PlayerPrefs.GetString("MySkins");
    }
    
    public static float GetSelectedSkinId()
    {
        return PlayerPrefs.GetInt("SelectedSkinId");;
    }
    
    public static int GetScore()
    {
        return PlayerPrefs.GetInt("Score");
    }
    
    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }

    #region Upgrades

    public static int GetUpgradeDamageCost()
    {
        return PlayerPrefs.GetInt("UpgradeDamageCost");
    }
    
    public static int GetUpgradeDamageValue()
    {
        return PlayerPrefs.GetInt("UpgradeDamageValue");
    }
    
    public static int GetUpgradeAttackSpeedCost()
    {
        return PlayerPrefs.GetInt("UpgradeAttackSpeedCost");
    }
    
    public static float GetUpgradeAttackSpeedValue()
    {
        return PlayerPrefs.GetFloat("UpgradeAttackSpeedValue");
    }

    public static int GetFpsToggleStatus()
    {
        return PlayerPrefs.GetInt("SettingsFPSToggle");
    }

    #endregion
    
    #endregion

    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
