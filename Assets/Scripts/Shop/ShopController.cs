using System;
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
        foreach (var shopItem in _shopItems)
        {
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
        _mySkins.PlayerSkins.Add(skinView); //todo: bug
        skinView.IsUnlocked = true;
        skinView.IsLockedImage.gameObject.SetActive(false);
        _infoPanelView.BuyButton.gameObject.SetActive(false);
        _infoPanelView.SelectButton.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _scoreView.Score = PlayerPrefs.GetInt("Score");
    }
}
