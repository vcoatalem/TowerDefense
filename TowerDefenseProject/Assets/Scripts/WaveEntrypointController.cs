using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEntrypointController : MonoBehaviour
{
    private Vector2 gridPosition;
    public Vector2 GetGridPosition => gridPosition;

    private List<Wave> waves; //TODO: later
    public List<Wave> GetWaves => waves;


    private NexusController targetNexus;
    private List<Vector2> pathToNexus;

    private List<EnemyController> enemies;

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
        waves = new List<Wave>();
        waves.Add(new Wave(new Dictionary<EnemyTemplate.EnemyTypes, int> //TODO: different waves later
        { 
            {
                EnemyTemplate.EnemyTypes.ENEMY1, 2
            } 
        }, 1));
        enemies = new List<EnemyController>();
        StartSpawning();
    }

    IEnumerator Spawn()
    {
        if (waves.Count == 0)
        {
            Debug.Log("No more waves to spawn...");
            yield return null;
        }

        Wave wave = waves[0];
        Debug.Log("Will spawn wave: " + wave.ToString());

        while (!wave.isOver())
        {
            EnemyTemplate toSpawn = waves[0].NextEnemy();

            GameObject instantiated = (GameObject)Instantiate(toSpawn.model, new Vector3(gridPosition.x, 1.5f, gridPosition.y), Quaternion.identity, transform);
            EnemyController enemy = instantiated.GetComponent<EnemyController>();
            enemy.Initialize(toSpawn, new List<Vector2>(pathToNexus));
            enemies.Add(enemy);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        waves.Remove(wave);
        Debug.Log("Done spawning wave. " + waves.Count + " remaining");
    }

    public void StartSpawning() 
    {
        StartCoroutine("Spawn");
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
        foreach (EnemyController enemy in enemies)
        {
            enemy.Behave();
        }
    }
}
