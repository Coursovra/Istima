using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfoPanelView : MonoBehaviour
{
    public SkinButtonView SkinButton { get; set; }
    public TMP_Text ItemNameText => _itemNameText;
    public TMP_Text ItemDescriptionText => _itemDescriptionText;
    [SerializeField] public Button SelectButton;
    [SerializeField] public Button BuyButton;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    //[SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject;
    public static event Action<SkinButtonView> OnBuyButtonClicked = delegate(SkinButtonView skinButton) { };
    public static event Action<SkinButtonView> OnSelectButtonClicked = delegate(SkinButtonView skinButton) { };

    public void BuyButtonClicked()
    {
        OnBuyButtonClicked.Invoke(SkinButton);
    }
    
    public void SelectButtonClicked()
    {
        var playerSpriteView = FindObjectOfType<PlayerSpriteController>();
        playerSpriteView.SetSelectedSkin(SkinButton.GetSkinView().gameObject);
        OnSelectButtonClicked.Invoke(SkinButton);
        //_selectedSkinScriptableObject.SelectedSkin = SkinButton.GetSkinInfoScriptableObject();
        gameObject.SetActive(false);
    }
}
