using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    CharacterController _characterController;
    PlayerInput _playerInput;
    public Animator _animator;

    public LayerMask layerMask;
    public Transform CrossHair;

    //movement
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    bool _isMovementPressed;
    int _isRunningHash;
    bool _isGrounded;
    
    //jumping
    bool _isJumpPressed = false;
    bool _isJumping;
    Coroutine _currentJumpResetCoroutine;
    float _initialJumpVelocity;
    float _maxJumpHeight = .3f;
    float _maxJumpTime = .4f;
    int _isJumpingHash;
    bool _requireNewJumpPress = false;

    //gravities
    float _groundedGravity = -0.05f;
    float _gravity = -5f;

    //movement related
    float _rotationFactorPerFrame = 15.0f;
    public float _speed = 6f;

    //state variables
    PlayerBaseState _currentState;
    StateFactory _states;

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Animator Animator { get { return _animator; } }
    public bool IsMovementPressed { get {  return _isMovementPressed; } }
    public float CurrentMovementInputX { get { return _currentMovementInput.x; } set { _currentMovementInput.x = value; } }
    public float CurrentMovementInputY { get { return _currentMovementInput.y; } set { _currentMovementInput.y = value; } }
    public bool IsGrounded {  get { return _isGrounded; } }
    public int IsRunningHash { get { return _isRunningHash; }  set { _isRunningHash = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsJumping {  set { _isJumping = value; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public bool RequireNewJumpPress { get  { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float CurrentMovementX {  get { return _currentMovement.x; }  set { _currentMovement.x = value; } }
    public float CurrentMovementZ { get { return _currentMovement.z; } set { _currentMovement.z = value; } }
    public Coroutine CurrentJumpResetCoroutine { get { return _currentJumpResetCoroutine; } set { _currentJumpResetCoroutine = value; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
    public float GroundedGravity {  get { return _groundedGravity; } }
    public float Gravity { get { return _gravity; } }
    public float Speed { get { return _speed; } }

    void Awake()
    {

        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();

        _states = new StateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;

        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;

        _isJumpingHash = Animator.StringToHash("isJumping");
        _isRunningHash = Animator.StringToHash("isRunning");

        SetUpJumpVariables();
    }

    void SetUpJumpVariables()
    {
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(_maxJumpTime/2, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / (_maxJumpTime/2);
    }

    void handleAnimations()
    {
        bool isRunning = _animator.GetBool(_isRunningHash);
        bool isJump = _animator.GetBool(_isJumpingHash);

        if (_isMovementPressed && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        else if (!_isMovementPressed && isRunning) 
        {
            _animator.SetBool(_isRunningHash, false);
        }
    }

    void Update()
    {
        handleRotation();
        _currentState.UpdateStates();
        _characterController.Move(_currentMovement * Time.deltaTime * _speed);

        //_isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, layerMask);
        _isGrounded = _characterController.isGrounded;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
