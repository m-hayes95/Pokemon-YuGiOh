using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)] private float maxSpeed;
    [SerializeField, Range(1.5f, 5f)] private float sprintMultiplier;
    
    private PlayerAnimator _playerAnimator;
    
    private float _speed;
    private bool _isSprinting;
    private bool _isFacingLeft;

    private const int IdleAnimationIndex = 0;
    private const int WalkAnimationIndex = 1;
    private const int SprintAnimationIndex = 2;

#region Unity Event Functions

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }
    private void Start()
    {
        _speed = maxSpeed;
    }
    private void Update()
    {
        KeyboardControls();
        GamepadControls();
        if (!_isSprinting)
            _speed = maxSpeed;
        Debug.Log($"current speed {_speed}");
    }
    #endregion
    
    #region Input Controls
    private void KeyboardControls()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;
        Vector2 inputVector = new Vector2(0f,0f);
        if (keyboard.wKey.isPressed)
        {
            inputVector.y =+ 1f;
        }
        if (keyboard.sKey.isPressed)
        {
            inputVector.y =- 1f;
        }
        if (keyboard.aKey.isPressed)
        {
            inputVector.x =- 1f;
        }
        if (keyboard.dKey.isPressed)
        {
            inputVector.x =+ 1f;
        }
        inputVector = inputVector.normalized;
        Vector3 moveWasdNormalised = new Vector3(inputVector.x, 0f, inputVector.y);
        Move(moveWasdNormalised);
        if (keyboard.shiftKey.isPressed)
        {
            Sprint();
        } else 
            _isSprinting = false;
        if (keyboard.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }
    private void GamepadControls()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return;
        if (gamepad.aButton.wasPressedThisFrame)
        {
            Interact();
        }

        if (gamepad.bButton.isPressed)
        {
            Sprint();
        }
        else
            _isSprinting = false;
        Vector2 moveLeftStick = gamepad.leftStick.ReadValue().normalized;
        Vector3 moveDir = new Vector3(moveLeftStick.x, 0f, moveLeftStick.y);
        Move(moveDir);
    }
    #endregion
    
    #region Interactions
    // ReSharper disable Unity.PerformanceAnalysis
    private void Interact()
    {
        // Interact
        Debug.Log($"Player Interacted");
    }

    private void Sprint()
    {
        if (_isSprinting) return;
        _isSprinting = true;
        _speed *= sprintMultiplier;
    }
    private void Move(Vector3 direction)
    {
        transform.position += direction * (_speed * Time.deltaTime);
        
        if (_isFacingLeft && direction.x > 0) TurnPlayer();
        if (!_isFacingLeft && direction.x < 0) TurnPlayer();
        
        if (direction == Vector3.zero)
        {
            _playerAnimator.ChangeAnimationState(IdleAnimationIndex);
        }
        switch (_isSprinting)
        {
            case true when direction != Vector3.zero:
                _playerAnimator.ChangeAnimationState(WalkAnimationIndex);
                break;
            case false when direction != Vector3.zero:
                _playerAnimator.ChangeAnimationState(SprintAnimationIndex);
                break;
        }
    }
    private void TurnPlayer()
    {
        transform.Rotate(0f, 180f, 0f);
        _isFacingLeft = !_isFacingLeft;
    }
    #endregion
}
}

