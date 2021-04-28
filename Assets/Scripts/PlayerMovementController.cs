using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private readonly float _speed = .36f;
    
    void Update()
    {
        if (PlayerController.IsPlaying)
        {
            MovementHandler();
        }
    }

    private void MovementHandler()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                var x = Input.touches[0].deltaPosition.x * _speed * Time.deltaTime;

                transform.Translate(new Vector3(x, 0, 0));
            }
        }
    }
}