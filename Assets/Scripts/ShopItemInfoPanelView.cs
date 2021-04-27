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
    [SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject;
    public event Action<SkinButtonView> OnBuyButtonClicked = delegate(SkinButtonView skinButton) { };
    public event Action<SkinButtonView> OnSelectButtonClicked = delegate(SkinButtonView skinButton) { };

    public void BuyButtonClicked()
    {
        OnBuyButtonClicked.Invoke(SkinButton);
    }
    
    public void SelectButtonClicked()
    {
        var playerSpriteView = FindObjectOfType<PlayerSpriteView>();
        playerSpriteView.SetSprite(SkinButton.GetImage().sprite);
        OnSelectButtonClicked.Invoke(SkinButton);
        _selectedSkinScriptableObject.SelectedSkin = SkinButton.GetSkinInfoScriptableObject();
    }
}
