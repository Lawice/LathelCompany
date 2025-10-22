using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Stats")]
    [SerializeField] private float _playerSpeed;
    
    private Transform _transform;
    private PlayerInputsManager _playerInputsManager;
    
    
    void Start()
    {
        _playerInputsManager = GetComponent<PlayerInputsManager>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        OnMove(_playerInputsManager.MoveValue);
    }

    void OnMove(Vector2 value)
    {
        _transform.position += new Vector3(value.x, 0, value.y) * _playerSpeed;
    }
}
