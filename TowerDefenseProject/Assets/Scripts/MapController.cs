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
    private List<NexusController> nexuses = new List<NexusController>();
    private List<WaveEntrypointController> waveEntrypoints = new List<WaveEntrypointController>();

    private List<Vector2> forbiddenTurretPlacementCells = new List<Vector2>();

    private Dictionary<string, Object> prefabs;
    void Awake()
    {
        prefabs = new Dictionary<string, Object>()
        {
            { "tile", Resources.Load("Prefabs/RegularTile") },
            { "nexus",  Resources.Load("Prefabs/NexusTile") },
            { "entry",  Resources.Load("Prefabs/EntryTile") },
            { "turret1", Resources.Load("Prefabs/Turret1") }
        };  
    }

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = file.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None) ;
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
        var convert = new Dictionary<MapController.CellState, Object>()
        {
            { MapController.CellState.ENTRY, prefabs["entry"] },
            { MapController.CellState.NEXUS, prefabs["nexus"] },
            { MapController.CellState.EMPTY, prefabs["tile"] },
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
                    if (grid[i, j] == CellState.NEXUS)
                    {
                        nexuses.Add(instantiated.GetComponent<NexusController>());
                    }
                    if (grid[i, j] == CellState.ENTRY)
                    {
                        waveEntrypoints.Add(instantiated.GetComponent<WaveEntrypointController>());
                    }
                }
            }
        }
        //TEST
        PlaceTurret(new Vector2(3, 3), prefabs["turret1"]);
        waveEntrypoints.ForEach((entry => entry.Initialize(grid, nexuses[0])));
    }

    private void ComputeForbiddenTurretPlacementCells()
    {
        forbiddenTurretPlacementCells = new List<Vector2>();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == CellState.EMPTY)
                {
                    grid[i, j] = CellState.TURRET;
                    foreach (WaveEntrypointController entry in waveEntrypoints)
                    {
                        if (entry.ComputePathToTargetNexus(grid) == null)
                        {
                            forbiddenTurretPlacementCells.Add(new Vector2(i, j));
                        }
                    }
                }
            }
        }
    }


    public void PlaceTurret(Vector2 gridPosition, Object turretPrefab)
    {
        if (forbiddenTurretPlacementCells.Contains(gridPosition))
        {
            Debug.Log("Can't place a turret there as it would block enemy movements");
            return;
        }

        grid[(int)gridPosition.y, (int)gridPosition.x] = CellState.TURRET;
        GameObject instantiated = (GameObject)Instantiate(turretPrefab, new Vector3(gridPosition.x, 1.5f, gridPosition.y), Quaternion.identity, transform);

        foreach (WaveEntrypointController entry in waveEntrypoints)
        {
            if (entry.GetPathToNexus.Contains(gridPosition))
            {
                ComputeForbiddenTurretPlacementCells();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
