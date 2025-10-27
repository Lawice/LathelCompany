using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private float _offset;
    [SerializeField] private int _maxRooms = 40;

    [Range(0f, 1f)] [SerializeField] private float _branchChance = 0.7f;
    [Range(1, 4)] [SerializeField] public int _maxEntrancesPerRoom = 4;

    private Dictionary<Vector2Int, Room> _grid = new();

    private readonly Vector2Int[] directions = new Vector2Int[]{
        new(1, 0),  // Up 
        new(-1, 0),  // Down
        new(0, -1),  // Right
        new(0, 1), // Left
    };

    private void Start(){
        GenerateMap();
    }

    private void GenerateMap(){
        Queue<Vector2Int> toCreate = new();
        Vector2Int start = Vector2Int.zero;
        Room startRoom = Instantiate(_roomPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Room>();
        _grid[start] = startRoom;
        toCreate.Enqueue(start);

        int created = 1;

        while (toCreate.Count > 0 && created < _maxRooms){
            Vector2Int current = toCreate.Dequeue();
            Room currentRoom = _grid[current];

            for (int i = 0; i < 4; i++){
                if (CountOpenEntrances(currentRoom) >= _maxEntrancesPerRoom)
                    break;

                if (Random.value > _branchChance)
                    continue;

                Vector2Int dir = directions[i];
                Vector2Int newPos = current + dir;

                if (_grid.ContainsKey(newPos)){
                    continue;
                }
                
                Vector3 worldPos = new Vector3(newPos.x * _offset, 0, newPos.y * _offset);
                Room newRoom = Instantiate(_roomPrefab, worldPos, Quaternion.identity, transform).GetComponent<Room>();
                _grid[newPos] = newRoom;
                created++;
                
                currentRoom.EntranceStates[i] = EntranceState.Open;
                int opposite = OppositeIndex(i);
                newRoom.EntranceStates[opposite] = EntranceState.Open;

                toCreate.Enqueue(newPos);

                if (created >= _maxRooms)
                    break;
            }
        }
        
        foreach (KeyValuePair<Vector2Int, Room> cell in _grid){
            Vector2Int pos = cell.Key;
            Room room = cell.Value;

            for (int i = 0; i < 4; i++){
                Vector2Int neighborPos = pos + directions[i];
                if (_grid.TryGetValue(neighborPos, out Room neighbor)){
                    int opposite = OppositeIndex(i);
                    
                    if (room.EntranceStates[i] == EntranceState.Open && neighbor.EntranceStates[opposite] != EntranceState.Open)
                        room.EntranceStates[i] = EntranceState.Locked;

                    else if (neighbor.EntranceStates[opposite] == EntranceState.Open)
                        room.EntranceStates[i] = EntranceState.Open;
                }
                else{
                    // ✅ Pas de voisin → fermé
                    room.EntranceStates[i] = EntranceState.Locked;
                }
            }

            room.UpdateRoom();
        }

        DrawDebugConnections();
    }

    private int OppositeIndex(int index){
         return index switch {
                0 => 1, // Up ↔ Down
                1 => 0,
                2 => 3, // Right ↔ Left
                3 => 2,
                _ => -1
         };
    }

    private int CountOpenEntrances(Room currentRoom){
        return currentRoom.EntranceStates.Count(entrance => entrance == EntranceState.Open);
    }
    
    private void DrawDebugConnections()
    {
        foreach (var kvp in _grid)
        {
            Vector2Int pos = kvp.Key;
            Room room = kvp.Value;
            Vector3 roomPos = room.transform.position + Vector3.up * 0.5f;

            // Dessine le centre de la salle (bleu)
            Debug.DrawRay(roomPos, Vector3.up * 0.5f, Color.blue, 5222);

            // Dessine des traits vers les salles connectées
            for (int i = 0; i < 4; i++)
            {
                if (room.EntranceStates[i] == EntranceState.Open)
                {
                    Vector2Int neighborPos = pos + directions[i];
                    if (_grid.TryGetValue(neighborPos, out Room neighbor))
                    {
                        Vector3 neighborPos3D = neighbor.transform.position + Vector3.up * 0.5f;
                        Debug.DrawLine(roomPos, neighborPos3D, Color.green,5222);
                    }
                    else
                    {
                        // Si pas de voisin mais entrée ouverte => ligne rouge = erreur potentielle
                        Vector3 dir = new Vector3(directions[i].x, 0, directions[i].y);
                        Debug.DrawRay(roomPos, dir * (_offset * 0.5f), Color.red, 5222);
                    }
                }
            }
        }
    }

}