using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Level0Generator : MapGenerator
{
    public GameObject testObj;

    GeneratedTile inn, treasure, boss;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SecondPass();
        DebugInstantiate();
    }

    protected void SecondPass()
    {
        // find locations to place additional rooms. 

        inn = PlaceSpecialRoom(3, TILE_TYPE.INN);
        treasure = PlaceSpecialRoom(2, TILE_TYPE.TREASURE);
        boss = PlaceSpecialRoom(1, TILE_TYPE.BOSS);

        _finalTiles.Add(inn);
        _finalTiles.Add(treasure);
        _finalTiles.Add(boss);
    }

    protected bool CheckAroundTile(GeneratedTile curTile)
    {
        int adjNum = 0;

        if (!CheckLocation(new Vector2Int(curTile.x + 1, curTile.y)))
            ++adjNum;
        if (!CheckLocation(new Vector2Int(curTile.x - 1, curTile.y)))
            ++adjNum;
        if (!CheckLocation(new Vector2Int(curTile.x, curTile.y + 1)))
            ++adjNum;
        if (!CheckLocation(new Vector2Int(curTile.x, curTile.y - 1)))
            ++adjNum;

        return adjNum < 2;
    }

    private GeneratedTile PlaceSpecialRoom(int divisor, TILE_TYPE type)
    {
        int loc = (_finalTiles.Count-1) / divisor;

        Vector2Int curloc = new Vector2Int(_finalTiles[loc].x, _finalTiles[loc].y);

        Stack<GeneratedTile> gtiles = new Stack<GeneratedTile>();

        int placed = 0;
        int attempts = 3;
        
        while(placed == 0)
        {
            gtiles.Clear();
            gtiles.Push(_finalTiles[loc]);
            for (int i = 1; i < attempts; ++i)
            {
                if (loc + i >= _finalTiles.Count)
                    break;
                gtiles.Push(_finalTiles[loc + i]);
            }

            for (int i = 1; i < attempts; i++)
            {
                if (loc - i < 0)
                    break;
                gtiles.Push(_finalTiles[loc - i]);
            }

            FindAndPlace(ref gtiles, curloc, ref placed, type);

            ++attempts;
        }

        return gtiles.Pop();
    }

    protected void DebugInstantiate()
    {
        foreach(var t in _finalTiles)
        {
            GameObject newObj = Instantiate(testObj);
            newObj.transform.position = new Vector3(t.x * 10, t.y * 10, 0);
            newObj.transform.SetParent(transform);

            switch(t.type)
            {
                case TILE_TYPE.NORMAL:
                    break;
                case TILE_TYPE.INN:
                    newObj.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.6f, 0.1f);
                    newObj.name = "INN";
                    break;
                case TILE_TYPE.BOSS:
                    newObj.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f);
                    newObj.name = "BOSS";
                    break;
                case TILE_TYPE.TREASURE:
                    newObj.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.0f);
                    newObj.name = "TREASURE";
                    break;
            }
        }
    }
}
