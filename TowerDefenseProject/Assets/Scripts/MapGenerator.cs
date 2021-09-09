using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    private Object entryTile;
    private Object regularTile;
    private Object nexusTile;

    void Start()
    {
        entryTile = Resources.Load("Prefabs/EntryTile");
        regularTile = Resources.Load("Prefabs/RegularTile");
        nexusTile = Resources.Load("Prefabs/NexusTile");
    }

    public void Generate(MapController.CellState[,] grid)
    {
        var convert = new Dictionary<MapController.CellState, Object>()
        {
            { MapController.CellState.ENTRY, entryTile },
            { MapController.CellState.NEXUS, nexusTile },
            { MapController.CellState.EMPTY, regularTile },
            { MapController.CellState.OOB, null },
            { MapController.CellState.TURRET, null }
        };
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Object toInstanciate = convert[grid[i, j]];
                if (toInstanciate)
                {
                    Instantiate(toInstanciate, new Vector3(i, 1, j), Quaternion.identity, transform);
                }
            }
        }
    }
}
