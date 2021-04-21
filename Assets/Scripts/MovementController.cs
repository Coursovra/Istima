using UnityEngine;

public class MovementController : MonoBehaviour
{
    private PlayerView _playerView;
    void Start()
    {
        
    }
    
    void Update()
    {
        //MovementHandler(_playerView);
    }

    private void MovementHandler(PlayerView player)
    {
        var touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        player.transform.position = touchPosition;
    }
}
