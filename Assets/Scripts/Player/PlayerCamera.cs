using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour
{
    [FormerlySerializedAs("_playerBody")]
    [Header("References")]
    [SerializeField] private Transform _orientation;

    [Header("Camera Settings")]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;
    [SerializeField] private float _clampAngle;
    
    private float _xRotation;
    private float _yRotation;

    private PlayerInputsManager _playerInput;
    
    private void Start()
    {
        _playerInput = GetComponentInParent<PlayerInputsManager>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Looking(_playerInput.LookValue);
    }

    private void Looking(Vector2 value)
    {
        float mouseX = value.x * _sensitivityX;
        float mouseY = value.y * _sensitivityY;

        _xRotation += mouseX;
        
        _yRotation -= mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -_clampAngle, _clampAngle);
        
        transform.rotation = Quaternion.Euler(_yRotation, _xRotation, 0f);
        
        _orientation.rotation = Quaternion.Euler(0f, _xRotation, 0f);
    }
}