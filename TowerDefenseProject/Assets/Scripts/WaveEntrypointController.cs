using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEntrypointController : MonoBehaviour
{
    private Vector2 gridPosition;
    public Vector2 GetGridPosition => gridPosition;

    private List<Wave> waves = new List<Wave>();
    public List<Wave> GetWaves => waves;


    private NexusController targetNexus;
    private List<Vector2> pathToNexus = new List<Vector2>();
    public List<Vector2> GetPathToNexus => pathToNexus;

    private List<EnemyController> enemies = new List<EnemyController>();

    private Object pathMarker;

    private Dictionary<EnemyController.EnemyTypes, Object> enemyPrefabs;

    void Awake()
    {
        pathMarker = Resources.Load("Prefabs/Cylinder");
        enemyPrefabs = new Dictionary<EnemyController.EnemyTypes, Object>()
        {
            { EnemyController.EnemyTypes.ENEMY1, Resources.Load("Prefabs/Enemy1") },
            { EnemyController.EnemyTypes.ENEMY2, Resources.Load("Prefabs/Enemy2") },
            { EnemyController.EnemyTypes.ENEMY3, Resources.Load("Prefabs/Enemy3") }
        };
    }

    public void Initialize(MapController.CellState[,] grid, NexusController target)
    {
        gridPosition = new Vector2(transform.position.x, transform.position.z); //TODO: for now we will do this assumption
        targetNexus = target;
        UpdatePathToTargetNexus(grid);
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
            EnemyController.EnemyTypes toSpawn = waves[0].NextEnemy();

            GameObject instantiated = (GameObject)Instantiate(enemyPrefabs[toSpawn], new Vector3(gridPosition.x, 1.5f, gridPosition.y), Quaternion.identity, transform);
            EnemyController enemy = instantiated.GetComponent<EnemyController>();
            enemy.SetPath(new List<Vector2>(pathToNexus));
            enemies.Add(enemy);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        waves.Remove(wave);
        Debug.Log("Done spawning wave. " + waves.Count + " remaining");
    }

    public void EnqueueWave()
    {
        waves.Add(new Wave(new Dictionary<EnemyController.EnemyTypes, int> //TODO: different waves later
        {
            { EnemyController.EnemyTypes.ENEMY1, 3 },
            { EnemyController.EnemyTypes.ENEMY3, 2 },
            { EnemyController.EnemyTypes.ENEMY2, 4 },

        }, 0.5f));
        StartSpawning();
    }

    public void StartSpawning() 
    {
        StartCoroutine("Spawn");
    }

    public void UpdatePathToTargetNexus(MapController.CellState[,] grid)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        pathToNexus = ComputePathToTargetNexus(grid);
        if (pathToNexus != null)
        {
            pathToNexus.ForEach((vect) =>
            {
                //Debug.Log(vect);
                Instantiate(pathMarker, new Vector3(vect.x, 1.2f, vect.y), Quaternion.identity, transform);
            });
        }
    }

    public List<Vector2> ComputePathToTargetNexus(MapController.CellState[,] grid) //pathfinding algorithm
    {
        List<Vector2> pathToNexus = Pathfinding.FindPath(grid, gridPosition, targetNexus.GetGridPosition);
        if (pathToNexus == null)
        {
            //Debug.Log("Could not find path to target Nexus: " + targetNexus.GetGridPosition.ToString());
        }
        return pathToNexus;
    }

    void Update()
    {
        enemies = enemies.Where((enemy) => enemy != null).ToList();
        enemies.ForEach(enemy => enemy.Behave());
    }
}
