using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Stats")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private bool _isSprinting;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    
    [SerializeField] private Vector3 _boxCastRange;
    [SerializeField] private LayerMask _groundLayer;
    
    private Transform _transform;
    private Rigidbody _rigidbody;
    private PlayerInputsManager _playerInputsManager;
    private Collider[] _groundColliders = new Collider[10];
    
    
    void Start()
    {
        _playerInputsManager = GetComponent<PlayerInputsManager>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInputsManager.OnSprintEvent.Performed += OnSprint;
        _playerInputsManager.OnSprintEvent.Canceled += CancelSprint;
        _playerInputsManager.OnJumpEvent.Performed += OnJump;
    }

    void Update()
    {
        OnMove(_playerInputsManager.MoveValue);
        GroundCheck();
    }

    void GroundCheck()
    {
        int size = Physics.OverlapBoxNonAlloc(_transform.position, _boxCastRange, _groundColliders, Quaternion.identity, _groundLayer);
        _isGrounded = size > 0;
        
    }

    void OnMove(Vector2 value)
    {
        _transform.position += _isSprinting? new Vector3(value.x, 0, value.y) * _sprintSpeed  :  new Vector3(value.x, 0, value.y) * _playerSpeed;
    }

    void OnJump()
    {
        if (_isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce);
    }

    void OnSprint()
    {
        _isSprinting = true;
    }

    void CancelSprint()
    {
        _isSprinting = false;
    }
}
