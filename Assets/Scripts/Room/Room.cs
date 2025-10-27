using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{
    public List<GameObject> Walls; //0-Up, 1-Down, 2-Right, 3-Left
    public List<GameObject> Doors;

    public List<EntranceState> EntranceStates;

    private void Start(){
        UpdateRoom(EntranceStates);
    }


    private void UpdateRoom(List<EntranceState> states){
        for (int u = 0; u < states.Count; u++){
            switch (states[u]){
                case EntranceState.Open:
                    Doors[u].SetActive(true);
                    Walls[u].SetActive(false);
                    break;
                case EntranceState.Closed:
                    Doors[u].SetActive(true);
                    Walls[u].SetActive(false);
                    Debug.Log("Closed");
                    break;
                case EntranceState.Locked:
                    Doors[u].SetActive(false);
                    Walls[u].SetActive(true);
                    break;
            }
            
        }
    }
    
}

public enum EntranceState{
    Open,
    Closed,
    Locked
}
