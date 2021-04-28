using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool IsPlaying;
    [SerializeField] private UIController _uiController;
    [SerializeField] private ObstacleController _obstacleController;
    private readonly Vector2 _startPosition = new Vector2(0, -8);
    private void OnMouseDown()
    {
        if (!IsPlaying)
        {
            SwitchUi();
            _uiController.DeathScreen.SetActive(false);
            _uiController.UpgradePanel.SetActive(false);
            IsPlaying = true;
        }
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }
    
    private void SwitchUi()
    {
        _uiController.Menu.SetActive(false);
        _uiController.Ui.SetActive(true);
        _uiController.StartGameObjects.SetActive(false);
        _uiController.DeathScreen.SetActive(true);
        _uiController.UpgradePanel.SetActive(true);
        _obstacleController.gameObject.SetActive(true);
        _uiController.ShopButton.SetActive(false);
    }
}
