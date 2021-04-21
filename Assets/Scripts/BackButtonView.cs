using UnityEngine;

public class BackButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas; 
    [SerializeField] private GameObject buttonShop;

    public void HideShop()
    {
        _shopCanvas.SetActive(false);
        buttonShop.SetActive(true);
    }
}
