using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_TYPE
{
    NORMAL = 0,
    INN,
    TREASURE,
    BOSS
}

public class GeneratedTile
{
    public int x, y;
    public TILE_TYPE type;


    public GeneratedTile()
    {
        x = 0;
        y = 0;
        type = TILE_TYPE.NORMAL;
    }

    public GeneratedTile(int x, int y)
    {
        this.y = y;
        this.x = x;
        type = TILE_TYPE.NORMAL;
    }

    public GeneratedTile(Vector2Int i)
    {
        x = i.x;
        y = i.y;
        type = TILE_TYPE.NORMAL;
    }
}

public class GeneratedLevelMap
{
    public int Width, Height;
    public GeneratedTile[][] Map;

    public GeneratedLevelMap(int size)
    {
        Map = new GeneratedTile[size][];
        for (int i = 0; i < size; ++i)
            Map[i] = new GeneratedTile[size];

        Width = size;
        Height = size;
    }

    public void Clear()
    {
        System.Array.Clear(Map, 0, Map.Length);
    }
}

public class MapGenerator : MonoBehaviour
{
    public int Map_Size = 13;
    public int Rooms_Amount_Lower = 7;
    public int Rooms_Amount_Upper = 9;

    protected GeneratedLevelMap _map;
    protected List<GeneratedTile> _finalTiles = new List<GeneratedTile>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        RegenMap();
    }

    public virtual void RegenMap()
    {
        Random.SeedRandom();
        MapGenerationIsaac();
    }

    protected void Initialization()
    {
        _map = new GeneratedLevelMap(Map_Size);
    }

    public virtual void GenerationFirstPass()
    {
        Initialization();
        int roomsPlaced = 0;
        Vector2Int curLoc = new Vector2Int(Map_Size / 2, Map_Size / 2);
        int amountToPlace = Random.GetIntInclusive(Rooms_Amount_Lower, Rooms_Amount_Upper);

        PlaceRoom(curLoc, ref roomsPlaced);

        while (roomsPlaced < amountToPlace)
        {
            FindNewLoc(ref curLoc);
            PlaceRoom(curLoc, ref roomsPlaced);
        }
    }

    protected virtual void PlaceRoom(Vector2Int curLoc, ref int roomsPlaced)
    {
        GeneratedTile newTile = new GeneratedTile(curLoc.x, curLoc.y);
        _map.Map[curLoc.x][curLoc.y] = newTile;
        ++roomsPlaced;
        _finalTiles.Add(newTile);
    }

    protected virtual void FindNewLoc(ref Vector2Int curLoc)
    {
        bool complete = false;
        Vector2Int nextLoc = Vector2Int.zero;
        while (!complete)
        {
            var dir = Random.GetInt(4);
            nextLoc = curLoc;
            switch (dir)
            {
                case 0:
                    nextLoc.x--;
                    break;
                case 1:
                    nextLoc.x++;
                    break;
                case 2:
                    nextLoc.y--;
                    break;
                case 3:
                    nextLoc.y++;
                    break;
            }

            complete = CheckLocation(nextLoc);
        }

        curLoc = nextLoc;
    }

    protected virtual bool CheckLocation(Vector2Int curLoc)
    {
        return _map.Map[curLoc.x][curLoc.y] == null;
    }

    protected virtual void MapGenerationIsaac()
    {
        Initialization();

        Queue<GeneratedTile> activeTiles = new Queue<GeneratedTile>();
        Vector2Int curLoc = new Vector2Int(Map_Size / 2, Map_Size / 2);
        int amountToPlace = Random.GetIntInclusive(Rooms_Amount_Lower, Rooms_Amount_Upper);
        int roomsPlaced = 0;

        PlaceRoomQueue(curLoc, ref roomsPlaced, ref activeTiles);

        while (roomsPlaced < amountToPlace)
        {
            FindAndPlace(ref activeTiles, curLoc, ref roomsPlaced);
        }

        while(activeTiles.Count > 0)
        {
            _finalTiles.Add(activeTiles.Dequeue());
        }
    }

    protected virtual void FindAndPlace(ref Queue<GeneratedTile> tileQ, Vector2Int curLoc, ref int roomsPlaced)
    {
        int attempts = 0;
        int maxAttempts = 10;
        bool safe = false;
        List<GeneratedTile> potentials = new List<GeneratedTile>();
        foreach (var t in tileQ)
        {
            attempts = 0;
            while (attempts < maxAttempts)
            {
                Vector2Int nextLoc = CheckNextLocation(new Vector2Int(t.x, t.y));
                safe = CheckAdj(nextLoc);
                ++attempts;

                if (safe)
                {
                    potentials.Add(new GeneratedTile(nextLoc));
                }
            }
        }

        if (potentials.Count == 0)
            return;

        var picked = Random.GetRandomInList(potentials);
        tileQ.Enqueue(picked);
        ++roomsPlaced;
        _map.Map[picked.x][picked.y] = picked;
    }

    protected virtual void FindAndPlace(ref Stack<GeneratedTile> tileQ, Vector2Int curLoc, ref int roomsPlaced, TILE_TYPE tt)
    {
        int attempts = 0;
        int maxAttempts = 10;
        bool safe = false;
        List<GeneratedTile> potentials = new List<GeneratedTile>();
        foreach (var t in tileQ)
        {
            attempts = 0;
            while (attempts < maxAttempts)
            {
                Vector2Int nextLoc = CheckNextLocation(new Vector2Int(t.x, t.y));
                safe = CheckAdj(nextLoc);
                ++attempts;

                if (safe)
                {
                    potentials.Add(new GeneratedTile(nextLoc));
                }
            }
        }

        if (potentials.Count == 0)
            return;

        var picked = Random.GetRandomInList(potentials);
        picked.type = tt;
        tileQ.Push(picked);
        ++roomsPlaced;
        _map.Map[picked.x][picked.y] = picked;
    }

    protected Vector2Int CheckNextLocation(Vector2Int curLoc)
    {
        int dir = Random.GetInt(4);
        Vector2Int next = Vector2Int.zero;
        switch (dir)
        {
            case 0:
                next = new Vector2Int(curLoc.x + 1, curLoc.y);
                break;
            case 1:
                next = new Vector2Int(curLoc.x - 1, curLoc.y);
                break;
            case 2:
                next = new Vector2Int(curLoc.x, curLoc.y + 1);
                break;
            case 3:
                next = new Vector2Int(curLoc.x, curLoc.y - 1);
                break;
        }

        return next;
    }

    protected virtual bool CheckAdj(Vector2Int location)
    {
        int adjNum = 0;

        if (!CheckLocation(location))
            return false;

        if (!CheckLocation(new Vector2Int(location.x + 1, location.y)))
            ++adjNum;
        if (!CheckLocation(new Vector2Int(location.x - 1, location.y)))
            ++adjNum;
        if (!CheckLocation(new Vector2Int(location.x, location.y + 1)))
            ++adjNum;
        if (!CheckLocation(new Vector2Int(location.x, location.y - 1)))
            ++adjNum;

        return adjNum < 2;
    }

    protected virtual void PlaceRoomQueue(Vector2Int curLoc, ref int roomsPlaced, ref Queue<GeneratedTile> tileQ)
    {
        GeneratedTile newTile = new GeneratedTile(curLoc.x, curLoc.y);
        _map.Map[curLoc.x][curLoc.y] = newTile;
        ++roomsPlaced;
        tileQ.Enqueue(newTile);
    }

    protected virtual void ClearMap()
    {
        _finalTiles.Clear();
        if(_map != null)
            _map.Clear();
    }

    public GeneratedLevelMap GetMap()
    {
        return _map;
    }
}
