using UnityEngine;

public class BackButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas; 
    [SerializeField] private GameObject _buttonShop;
    [SerializeField] private GameObject _upgradePanel;

    public void HideShop()
    {
        _shopCanvas.SetActive(false);
        _buttonShop.SetActive(true);
        _upgradePanel.SetActive(true);
    }
}
