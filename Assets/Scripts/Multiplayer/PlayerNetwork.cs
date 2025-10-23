using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour {
    [SerializeField] private Camera _playerCamera;
    
    public override void OnNetworkSpawn(){
        Debug.Log("Connected");
        base.OnNetworkSpawn();
        if (IsOwner){
            UsePlayerCamera();
            Debug.Log("UseOwnerCamera");
        }
        else{
            if (_playerCamera != null){
                _playerCamera.gameObject.SetActive(false);
            }
        }
    }
    
    private void UsePlayerCamera(){
        Camera mainCam = Camera.main;
        if (mainCam != null && mainCam.gameObject != _playerCamera.gameObject){
            mainCam.gameObject.SetActive(false);
        }
        else{
            Debug.Log("WHy");
        }

        if (_playerCamera == null){
            Debug.Log("PlayerCamera is null");
            return;
        }
        
        _playerCamera.gameObject.SetActive(true);
        _playerCamera.enabled = true;
            
        AudioListener listener = _playerCamera.GetComponent<AudioListener>();
        if (listener != null){
            listener.enabled = true;
        }
    }
}
