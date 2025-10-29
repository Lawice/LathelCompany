using Unity.Netcode;
using UnityEngine;

public abstract class Interact : NetworkBehaviour
{
    [Header("Interact Settings")]
    public string interactPrompt = "Interact";

    public abstract void OnInteract(GameObject player);
}