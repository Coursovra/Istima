using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private PlayerModelScriptableObject _mySkins;
    [SerializeField] private ScoreView _scoreView;
    private SkinButtonView[] _shopItems;
    private ShopItemInfoPanelView _infoPanelView;
    
    void Start()
    {
        _infoPanelView = FindObjectOfType<ShopItemInfoPanelView>();
        _infoPanelView.OnBuyButtonClicked += InfoPanelViewOnBuyButtonClicked;
        _shopItems = FindObjectsOfType<SkinButtonView>();
        var counter = 0;
        foreach (var shopItem in _shopItems)
        {
            shopItem.Id = counter++;
            if (_mySkins.PlayerSkins.Contains(shopItem))
            {
                shopItem.IsUnlocked = true;
                shopItem.IsLockedImage.gameObject.SetActive(false);
            }
        }
        
        _infoPanelView.gameObject.SetActive(false);
    }

    private void InfoPanelViewOnBuyButtonClicked(SkinButtonView skinView)
    {
        if (_scoreView.Score < skinView.GetPrice() || skinView.IsUnlocked) { return; }
        BuySkin(skinView);
    }

    private void BuySkin(SkinButtonView skinView)
    {
        _scoreView.Score -= skinView.GetPrice();
        _mySkins.PlayerSkins.Add(skinView);
        skinView.IsUnlocked = true;
        skinView.IsLockedImage.gameObject.SetActive(false);
        _infoPanelView.BuyButton.gameObject.SetActive(false);
        _infoPanelView.SelectButton.gameObject.SetActive(true);
    }
}
