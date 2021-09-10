using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEntrypointController : MonoBehaviour
{
    private Vector2 gridPosition;
    public Vector2 GetGridPosition => gridPosition;

    private List<GameObject> waves; //TODO: later
    public List<GameObject> GetWaves => waves;


    private NexusController targetNexus;
    private List<Vector2> pathToNexus;

    private Object pathMarker;

    void Awake()
    {
        pathMarker = Resources.Load("Prefabs/Cylinder");
    }

    public void Initialize(MapController.CellState[,] grid, NexusController target)
    {
        gridPosition = new Vector2(transform.position.x, transform.position.z); //TODO: for now we will do this assumption
        targetNexus = target;
        FindPathToTargetNexus(grid, target);
    }


    public void Populate() //enqueue waves
    {
        //TODO: later
    }

    public void FindPathToTargetNexus(MapController.CellState[,] grid, NexusController target) //pathfinding algorithm
    {
        pathToNexus = Pathfinding.FindPath(grid, gridPosition, target.GetGridPosition);
        if (pathToNexus != null)
        {
            pathToNexus.ForEach((vect) =>
            {
                //Debug.Log(vect);
                Instantiate(pathMarker, new Vector3(vect.x, 1.2f, vect.y), Quaternion.identity, transform);
            });
        }
        else
        {
            Debug.Log("Could not find path to target Nexus: " + target.GetGridPosition.ToString());
        }
    }

    void Update()
    {

    }
}
