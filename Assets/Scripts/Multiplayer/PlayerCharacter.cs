using UnityEngine;
using Unity.Netcode;

public class PlayerCharacter : NetworkBehaviour {
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
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start(){
        NetworkManager.SceneManager.OnSceneEvent += OnSwitchScene;
    }

    private void OnSwitchScene(SceneEvent sceneEvent){
        if (sceneEvent.SceneEventType != SceneEventType.LoadComplete) return;
        SetPosition(Vector3.zero);
    }

    private void SetPosition(Vector3 position){
        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList){
            NetworkObject player = client.PlayerObject;
            if (player != null){
                player.transform.position = position;
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
