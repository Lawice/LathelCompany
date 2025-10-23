using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : NetworkBehaviour {
    public static SceneTransitionManager Instance;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestSceneChangeServerRpc(string sceneName){
        if (!IsServer){
            return;
        }
        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
