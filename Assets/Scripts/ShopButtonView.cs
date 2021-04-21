using UnityEngine;

public class ShopButtonView : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas; 
    
    public void ShowShop()
    {
        _shopCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
