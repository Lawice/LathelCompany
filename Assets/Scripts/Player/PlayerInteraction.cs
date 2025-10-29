using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : NetworkBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public Camera playerCamera;

    private PlayerInputsManager _playerInputs;
    private InputAction interactAction;

    void Awake()
    {
        _playerInputs = new PlayerControls();
    }

    void OnEnable()
    {
        _playerInputs.Player.Enable();
        interactAction = _playerInputs.Player.Pickup; // or rename the action to "Interact"
        interactAction.performed += OnInteract;
    }

    void OnDisable()
    {
        interactAction.performed -= OnInteract;
        _playerInputs.Player.Disable();
    }

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            Interact interactable = hit.collider.GetComponent<Interact>();
            if (interactable != null)
            {
                interactable.OnInteract(gameObject);
            }
        }
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            Interact interactable = hit.collider.GetComponent<Interact>();
            if (interactable != null)
            {
                // Show UI prompt (e.g., "Press E to Pick Up Flashlight")
                // UIManager.Instance.ShowPrompt("Press E to " + interactable.interactPrompt);
            }
        }
        else
        {
            // Hide UI prompt
            // UIManager.Instance.HidePrompt();
        }
    }
}