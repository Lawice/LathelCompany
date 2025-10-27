using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [FormerlySerializedAs("_playerSpeed")]
    [Header("Player Movement Stats")]
    [SerializeField] private float _normalSpeed;
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

    [SerializeField] private Transform _orientation;
    private Vector3 _moveDirection;
    
    
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
    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_transform.position, _boxCastRange);
    }
    void GroundCheck()
    {

        int size = Physics.OverlapBoxNonAlloc(_transform.position, _boxCastRange, _groundColliders, Quaternion.identity, _groundLayer);
        _isGrounded = size > 0;
    }

    void OnMove(Vector2 value)
    {
        _moveDirection = _orientation.forward * value.y + _orientation.right * value.x;
        if (!(_moveDirection.magnitude > 0.1f)){
            return;
        }
        if (_isSprinting){
            _rigidbody.AddForce(_moveDirection.normalized * _sprintSpeed, ForceMode.Force);
        }
        else{
            _rigidbody.AddForce(_moveDirection.normalized * _normalSpeed, ForceMode.Force);
        }
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
