using UnityEngine;

public class PlayerSpriteView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject;

    private void Start()
    {
        SetSprite(_selectedSkinScriptableObject.SelectedSkin.GetSkinInfoScriptableObject().Sprite);
    }
    
    public void SetSprite(Sprite sprite)
    {
        _playerSpriteRenderer.sprite = sprite;
    }
}
