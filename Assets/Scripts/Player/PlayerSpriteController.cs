using System;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private SkinView _selectedSkinView;
    [SerializeField] private ShopController _shopController;

    private GameObject _playerSkinInstance;
    
    public GameObject GetPlayerSkinInstance()
    {
        return _playerSkinInstance;
    }
    
    public SkinView GetSelectedSkinView()
    {
        return _selectedSkinView.GetComponent<SkinView>();
    }

    private void Awake()
    {
        foreach (var skinButton in _shopController.GetSkinButtons())
        {
            if (PlayerPrefsController.GetSelectedSkinId() == skinButton.GetSkinView().Id)
            {
                _selectedSkinView = skinButton.GetSkinView();
            }
        }

        _playerSkinInstance = Instantiate(_selectedSkinView.gameObject, transform);
    }
    
    public void SetSelectedSkin(GameObject skin)
    {
        Destroy(_playerSkinInstance);
        _playerSkinInstance = Instantiate(skin, transform);
        _selectedSkinView = skin.GetComponent<SkinView>();
        PlayerPrefsController.SetSelectedSkinId(skin.GetComponent<SkinView>().Id);
    }
}
