using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject StartGameObjects;
    public GameObject DeathScreen;
    public GameObject UpgradePanel;
    public GameObject ShopPanel;
    public GameObject ShopButton;
    public GameObject SettingsButton;
    public GameObject Slider;

    public void ToggleUi(bool value)
    {
        Slider.SetActive(value);
        DeathScreen.SetActive(!value);
        UpgradePanel.SetActive(!value);
        Menu.SetActive(!value);
        ShopButton.SetActive(!value);
        SettingsButton.SetActive(!value);
    }
}

