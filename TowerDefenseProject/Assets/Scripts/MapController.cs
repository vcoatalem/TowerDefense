using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public enum CellState
    {
        OOB,
        EMPTY,
        NEXUS,
        ENTRY,
        TURRET
    };

    public TextAsset file;
    private CellState[,] grid;
    private List<NexusController> nexuses;
    private List<WaveEntrypointController> waveEntrypoints;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = file.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None) ;
        nexuses = new List<NexusController>();
        waveEntrypoints = new List<WaveEntrypointController>();
        grid = new CellState[lines[0].Length, lines.Length];
        var convert = new Dictionary<char, CellState>()
        {
            { 'X', CellState.OOB },
            { 'E', CellState.ENTRY },
            { '-', CellState.EMPTY },
            { 'N', CellState.NEXUS },
            { 'T', CellState.TURRET }
        };
        foreach (var indexLine in lines.Select((line, index) => new { index, line }))
        {
            foreach (var indexChar in indexLine.line.ToCharArray().Select((c, index) => new { index, c }))
            {
                try
                {
                    grid[indexChar.index, indexLine.index] = convert[indexChar.c];
                }
                catch (KeyNotFoundException e)
                {
                    Debug.Log("invalid map file: " + e.ToString());
                }

            }
        }
        Generate();
    }

    private void Generate()
    {
        Object entryTile = Resources.Load("Prefabs/EntryTile");
        Object regularTile = Resources.Load("Prefabs/RegularTile");
        Object nexusTile = Resources.Load("Prefabs/NexusTile");

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
                    GameObject instantiated = (GameObject)Instantiate(toInstanciate, new Vector3(i, 1, j), Quaternion.identity, transform);
                    if (toInstanciate == nexusTile)
                    {
                        nexuses.Add(instantiated.GetComponent<NexusController>());
                    }
                    if (toInstanciate == entryTile)
                    {
                        waveEntrypoints.Add(instantiated.GetComponent<WaveEntrypointController>());
                    }
                }
            }
        }
        //TEST
        PlaceTurret(new Vector2(3, 3), new Turret1());
        waveEntrypoints.ForEach((entry => entry.Initialize(grid, nexuses[0])));
    }


    public void PlaceTurret(Vector2 gridPosition, TurretTemplate template)
    {
        grid[(int)gridPosition.y, (int)gridPosition.x] = CellState.TURRET;
        GameObject instantiated = (GameObject)Instantiate(template.model, new Vector3(gridPosition.x, 1.5f, gridPosition.y), Quaternion.identity, transform);
        TurretController turret = instantiated.GetComponent<TurretController>();
        turret.Initialize(template);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
