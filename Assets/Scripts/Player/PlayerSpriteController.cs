using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject;
    private SpriteRenderer _playerSpriteRenderer;
    private GameObject _playerSkinInstance;

    // private void Start()
    // {
    //     print(1);
    //     _playerSkinInstance = Instantiate(_selectedSkinScriptableObject.SelectedSkin.SkinGameObject, transform);
    //     _playerSpriteRenderer = _playerSkinInstance.GetComponent<SpriteRenderer>();
    //     var sprite = _selectedSkinScriptableObject.
    //         SelectedSkin.SkinGameObject.GetComponent<SpriteRenderer>().sprite;
    //     SetSprite(sprite);
    // }

    public void SetSprite(Sprite sprite)
    {
        _playerSpriteRenderer.sprite = sprite;
    }

    public GameObject GetPlayerSkinInstance()
    {
        if (_playerSkinInstance == null)
        {
            _playerSkinInstance = Instantiate(_selectedSkinScriptableObject.SelectedSkin.SkinGameObject, transform);
            _playerSpriteRenderer = _playerSkinInstance.GetComponent<SpriteRenderer>();
            var sprite = _selectedSkinScriptableObject.SelectedSkin.SkinGameObject.GetComponent<SpriteRenderer>()
                .sprite;
            SetSprite(sprite);
        }

        return _playerSkinInstance;
    }
}
