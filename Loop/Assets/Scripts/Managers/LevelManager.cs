using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script contains where we are in the map, whether to regenerate, and the map data itself
/// as well as knowing about the generator. 
/// Remember, the generator is just a runner, this controls running (and re-running) the level generator) 
/// </summary>
public class LevelManager : MonoBehaviour
{
    public MapGenerator MapGen;

    protected GeneratedLevelMap _currentMap;

    protected Vector2 _playerLoc;
    // Start is called before the first frame update
    void Start()
    {
        HandleRegeneration();
    }

    protected void FindDefaultPlayerLoc()
    {
        int middle = MapGen.Map_Size / 2;
        _playerLoc = new Vector2(middle, middle);
    }

    // WHEN WE DEFEAT THE BOSS AND GO TO THE NEXT MAP
    public void GoToNextLevelEvent()
    {
        HandleRegeneration();
    }

    protected void HandleRegeneration()
    {
        MapGen = FindObjectOfType<MapGenerator>();
        MapGen.RegenMap();
        _currentMap = MapGen.GetMap();
        FindDefaultPlayerLoc();
    }
}
