using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Stats")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private bool _isSprinting;
    [SerializeField] private float _jumpforce;
    
    
    private Transform _transform;
    private Rigidbody _rigidbody;
    private PlayerInputsManager _playerInputsManager;
    
    
    void Start()
    {
        _playerInputsManager = GetComponent<PlayerInputsManager>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerInputsManager.OnSprintEvent.Performed += OnSprint;
        _playerInputsManager.OnSprintEvent.Canceled += CancelSprint;
        _playerInputsManager.OnSprintEvent.Performed += OnJump;
    }

    void Update()
    {
        OnMove(_playerInputsManager.MoveValue);
    }

    void OnMove(Vector2 value)
    {
        _transform.position += _isSprinting? new Vector3(value.x, 0, value.y) * _sprintSpeed  :  new Vector3(value.x, 0, value.y) * _playerSpeed;
    }

    void OnJump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpforce);
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
