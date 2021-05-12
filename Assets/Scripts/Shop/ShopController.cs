using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<string> _mySkins;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private SkinButtonView[] _shopItems;
    private ShopItemInfoPanelView _infoPanelView;
    
    void Start()
    {
        _infoPanelView = FindObjectOfType<ShopItemInfoPanelView>();
        ShopItemInfoPanelView.OnBuyButtonClicked += InfoPanelViewOnBuyButtonClicked;
        _shopItems = FindObjectsOfType<SkinButtonView>();
        GetMySkins();
        GetBoughtSkins();
        
        _infoPanelView.gameObject.SetActive(false);
    }

    public SkinButtonView[] GetSkinButtons()
    {
        return _shopItems;
    }

    private void OnDestroy()
    {
        ShopItemInfoPanelView.OnBuyButtonClicked -= InfoPanelViewOnBuyButtonClicked;
    }

    private void GetBoughtSkins()
    {
        foreach (var shopItem in _shopItems)
        {
            foreach (var skinString in _mySkins)
            {
                var skinStatsArray = skinString.Split('-');
                var id = skinStatsArray[0];
                var damage = skinStatsArray[1];
                var attackSpeed = skinStatsArray[2];
                
                if (id != shopItem.GetSkinView().Id.ToString()) continue;
                
                shopItem.IsUnlocked = true;
                shopItem.IsLockedImage.gameObject.SetActive(false);
                shopItem.PriceText.gameObject.SetActive(false);
                shopItem.GetSkinView().Damage = Convert.ToSingle(damage);
                shopItem.GetSkinView().AttackSpeed = Convert.ToSingle(attackSpeed);
            }
        }
    }

    private string GetMySkins()
    {
        var mySkins = PlayerPrefsController.GetMySkins();
        if(mySkins.Length == 0) { return ""; }
        string mySkinsString = "";
        foreach (var newString in mySkins.Split(';'))
        {
            if(newString.Length == 0) { continue; }
            var array = newString.Split('-');
            var id = array[0];
            var damage = array[1];
            var attackSpeed = array[2];
            _mySkins.Add($"{id}-{damage}-{attackSpeed}");
            
            mySkinsString += $"{id}-{damage}-{attackSpeed};";
        }

        return mySkinsString;
    }

    private void InfoPanelViewOnBuyButtonClicked(SkinButtonView skinView)
    {
        if (_scoreView.Score < skinView.GetPrice() || skinView.IsUnlocked) { return; }
        BuySkin(skinView);
    }

    private void BuySkin(SkinButtonView skinView)
    {
        _scoreView.Score -= skinView.GetPrice();
        
        var mySkinsString = "";
        mySkinsString = GetMySkins();
        mySkinsString += $"{skinView.Id}-{skinView.GetSkinView().Damage}-{skinView.GetSkinView().AttackSpeed};";

        PlayerPrefsController.SetMySkins(mySkinsString);
        
        skinView.IsUnlocked = true;
        skinView.IsLockedImage.gameObject.SetActive(false);
        _infoPanelView.BuyButton.gameObject.SetActive(false);
        _infoPanelView.SelectButton.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _scoreView.Score = PlayerPrefsController.GetScore();
    }
}
