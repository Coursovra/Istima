using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Окно с информацией о выбранном скине
/// </summary>
public class ShopItemInfoPanelView : MonoBehaviour
{
    public SkinButtonView SkinButton { get; set; }
    public TMP_Text ItemNameText => _itemNameText;
    public TMP_Text ItemDescriptionText => _itemDescriptionText;
    [SerializeField] public Button SelectButton;
    [SerializeField] public Button BuyButton;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    public static event Action<SkinButtonView> OnBuyButtonClicked = delegate(SkinButtonView skinButton) { };
    public static event Action<SkinButtonView> OnSelectButtonClicked = delegate(SkinButtonView skinButton) { };

    /// <summary>
    /// Событие покупки скина, при нажатии на кнопку покупки в панели информации о скине
    /// </summary>
    public void BuyButtonClicked()
    {
        OnBuyButtonClicked.Invoke(SkinButton);
    }
    
    /// <summary>
    /// Обработка выбора нового скина
    /// </summary>
    public void SelectButtonClicked()
    {
        var playerSpriteView = FindObjectOfType<PlayerSpriteController>();
        playerSpriteView.SetSelectedSkin(SkinButton.GetSkinView().gameObject);
        OnSelectButtonClicked.Invoke(SkinButton);
        //_selectedSkinScriptableObject.SelectedSkin = SkinButton.GetSkinInfoScriptableObject();
        gameObject.SetActive(false);
    }
}
