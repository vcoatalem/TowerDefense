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

    public MapGenerator generator;
    public TextAsset file;
    private CellState[,] grid;


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
                    grid[indexLine.index, indexChar.index] = convert[indexChar.c];
                }
                catch (KeyNotFoundException e)
                {
                    Debug.Log("invalid map file: " + e.ToString());
                }

            }
        }
        generator.Generate(grid);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
