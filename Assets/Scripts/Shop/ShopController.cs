using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление магазином скинов
/// </summary>
public class ShopController : MonoBehaviour
{
    [SerializeField] private List<string> _mySkins;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private SkinButtonView[] _shopItems;
    private ShopItemInfoPanelView _infoPanelView;
    
    /// <summary>
    /// Заполнение массива всеми скинами из магазина;
    /// Подписка события на покупку скина;
    /// Получение купленных скинов игрока.
    /// </summary>
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

    /// <summary>
    /// Отписка от события при уничтожении магазина
    /// </summary>
    private void OnDestroy() //todo: OnInvisible?
    {
        ShopItemInfoPanelView.OnBuyButtonClicked -= InfoPanelViewOnBuyButtonClicked;
    }

    /// <summary>
    /// Какие скины из магазины куплены у игрока.
    /// Получение информации из PlayerPrefs
    /// </summary>
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

    /// <summary>
    /// Получение скинов игрока, их характеристик
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Отображение информации о скине при нажатии на него
    /// </summary>
    /// <param name="skinView">Скин, на который нажал игрок</param>
    private void InfoPanelViewOnBuyButtonClicked(SkinButtonView skinView)
    {
        if (_scoreView.Score < skinView.GetPrice() || skinView.IsUnlocked) { return; }
        BuySkin(skinView);
    }

    /// <summary>
    /// Покупка скина.
    /// Запись в PlayerPrefs
    /// </summary>
    /// <param name="skinView"></param>
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

    /// <summary>
    /// Обновление счета при заходе в магазин
    /// </summary>
    private void OnEnable()
    {
        _scoreView.Score = PlayerPrefsController.GetScore();
    }
}
