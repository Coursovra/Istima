using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кнопка скина в магазине
/// </summary>
public class SkinButtonView : MonoBehaviour
{
    public int Id { get; set; }
    public bool IsUnlocked;
    public TMP_Text PriceText;
    [SerializeField] public Image IsLockedImage;
    [SerializeField] private Image _sprite;
    [SerializeField] private SkinView _skinView;
    //[SerializeField] private SkinInfoScriptableObject _info;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private ShopItemInfoPanelView _infoPanelView;
    private int _price;
    
    public SkinView GetSkinView()
    {
        return _skinView;
    }
    
    public int GetPrice()
    {
        return _price;
    }
    
    public Image GetImage()
    {
        return _sprite;
    }

    /// <summary>
    /// Установка текста 
    /// </summary>
    private void Start()
    {
        var spriteRenderer = _skinView.SpriteRenderer;
        //_infoPanelView = FindObjectOfType<ShopItemInfoPanelView>(); //bug: не находит (не активен GO)
        _price = (int) _skinView.Price;
        name = _skinView.Name;
        _nameText.text = _skinView.Name;
        PriceText.text = _skinView.Price.ToString();
        _sprite.sprite = spriteRenderer.sprite;
        Id = _skinView.Id;
    }

    /// <summary>
    /// Отображение информации о скине
    /// </summary>
    public void ShowInfoPanel()
    {
        _infoPanelView.gameObject.SetActive(true);
        SwitchButtonActive(IsUnlocked);
        _infoPanelView.SkinButton = this;
        _infoPanelView.ItemNameText.text = _skinView.Name;
        _infoPanelView.ItemDescriptionText.text = $"{_skinView.Description}\n \nУрон: {_skinView.Damage}\nСкорость атаки: {_skinView.AttackSpeed}\n";
    }

    /// <summary>
    /// Отображение кнопки покупки или выбора скина
    /// </summary>
    /// <param name="value"></param>
    private void SwitchButtonActive(bool value)
    {
        _infoPanelView.BuyButton.gameObject.SetActive(!value);
        _infoPanelView.SelectButton.gameObject.SetActive(value);
    }
}
