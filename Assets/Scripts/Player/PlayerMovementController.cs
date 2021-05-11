using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Slider _slider; 
    [SerializeField] private RectTransform _rectTransform; 
    [SerializeField] private SelectedSkinScriptableObject _selectedSkinScriptableObject; 
    private readonly float _speed = .36f;
    Vector3 _worldDimensions;

    private void Start()
    {
        _worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
        _slider.onValueChanged.AddListener(OnValueChanged);
        _slider.maxValue = _worldDimensions.x;
        _slider.minValue = -_worldDimensions.x;
        var spriteRenderer = _selectedSkinScriptableObject.SelectedSkin.SkinGameObject.GetComponent<SpriteRenderer>();
        Vector2 spriteSize = spriteRenderer.sprite.rect.size;
        _rectTransform.sizeDelta = new Vector2(spriteSize.x,_worldDimensions.y);
    }
    
    void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }
    
    // void Update()
    // {
    //     if (PlayerController.IsPlaying)
    //     {
    //         //MovementHandler();
    //     }
    // }

    private void MovementHandler()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                var x = Input.touches[0].deltaPosition.x * _speed * Time.deltaTime;
                //print(transform.position.x - x <= -_worldDimensions.x);
                if(transform.position.x - x <= -_worldDimensions.x || transform.position.x + x >= _worldDimensions.x) { return; }
                transform.Translate(new Vector3(x, 0, 0));
            }
        }
    }

    public void OnValueChanged(float value)
    {
        if (!PlayerController.IsPlaying) { return;}

        print($"pos: {transform.position}, newValue: {value}, diff: {transform.position.x - value}");
        //if (transform.position.x - value < 2f) { return;} 
        transform.position = new Vector2(value, transform.position.y);
    }
}