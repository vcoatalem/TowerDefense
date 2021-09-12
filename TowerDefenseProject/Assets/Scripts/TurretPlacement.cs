using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    private bool isPlacingTurret = false;
    public bool IsPlacingTurret => isPlacingTurret;

    private Object turret1;

    public MapController map;

    void Awake()
    {
        turret1 = Resources.Load("Prefabs/Turret1");
    }

    public void ToggleTurretPlacement()
    {
        isPlacingTurret = !isPlacingTurret;
    }

    public void PlaceTurret(Vector2 position)
    {
        map.PlaceTurret(position, turret1);
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
