using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButtonView : MonoBehaviour
{
    public int Id { get; set; }
    public bool IsUnlocked;
    [SerializeField] public Image IsLockedImage;
    [SerializeField] private Image _sprite;
    [SerializeField] private SkinInfoScriptableObject _info;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _damage;
    [SerializeField] private int _attackSpeed;
    [SerializeField] private ShopItemInfoPanelView _infoPanelView;
    private int _price;
    
    public int GetPrice()
    {
        return _price;
    }
    
    public Image GetImage()
    {
        return _sprite;
    }

    public SkinInfoScriptableObject GetSkinInfoScriptableObject()
    {
        return _info;
    }

    private void Start()
    {
        var sprite = _info.SkinGameObject.GetComponent<SpriteRenderer>().sprite;
        //_infoPanelView = FindObjectOfType<ShopItemInfoPanelView>(); //bug: не находит (не активен GO)
        _price = (int) _info.Price;
        name = _info.Name;
        _nameText.text = _info.Name;
        _priceText.text = _info.Price.ToString();
        _sprite.sprite = sprite;
        _damage = _info.Damage;
        _attackSpeed = _info.AttackSpeed;
        Id = _info.Id;
    }

    public void ShowInfoPanel()
    {
        _infoPanelView.gameObject.SetActive(true);
        SwitchButtonActive(IsUnlocked);
        _infoPanelView.SkinButton = this;
        _infoPanelView.ItemNameText.text = _info.Name;
        _infoPanelView.ItemDescriptionText.text = $"{_info.Description}\n \nУрон: {_damage}\nСкорость атаки: {_attackSpeed}\n";
    }

    private void SwitchButtonActive(bool value)
    {
        _infoPanelView.BuyButton.gameObject.SetActive(!value);
        _infoPanelView.SelectButton.gameObject.SetActive(value);
    }
}
