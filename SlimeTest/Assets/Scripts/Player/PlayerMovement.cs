using UnityEngine;
/// <summary>
/// Перемещение игрока по игровому полю
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    private const float MovementTime = 2.5f;
    private const float TimeWhenNeedToSlowDown = 0.7f;
    private const float MovementOffsetRight = 0.9f;
    private const float MovementOffsetLeft = -0.8f;
    private const float Speed = 3f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private MovementEffectsController _movementEffectsController;
    
    private float _timerForForward;

    public float MovementOffset{ get; private set; }
    

    private void FixedUpdate()
    {
        MovementController();
    }
    
    private void MovementController()
    {
        _rigidbody.AddForce(new Vector3(MovementOffset, 0.0f, 0.0f) * Speed, ForceMode.Impulse);

        if (_timerForForward > 0)
        {
            _timerForForward -= Time.deltaTime;

            if (_timerForForward > TimeWhenNeedToSlowDown)
            {
                MovementOffset = MovementOffsetRight;
            }
            else
            {
                MovementOffset = MovementOffsetLeft;
            }

            _playerAnimations.Run(true);
        }
        else
        {
            MovementOffset = 0;
            _playerAnimations.Run(false);
        }
    }
    
    public void StartMovement()
    {
        _timerForForward = MovementTime;
        _movementEffectsController.StartEffects();
    }
}