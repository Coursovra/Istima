using UnityEngine;

public class ShopButtonView : MonoBehaviour
{
    [SerializeField] private UIController _uiController;

    /// <summary>
    /// Открытие магазина
    /// </summary>
    public void ShowShop()
    {
        _uiController.ShopPanel.SetActive(true);
        gameObject.SetActive(false);
        _uiController.DeathScreen.SetActive(false);
        _uiController.UpgradePanel.SetActive(false);
        _uiController.Ui.SetActive(true);
    }
}
