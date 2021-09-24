using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{

    public enum TurretType
    {
        NONE,
        TURRET1,
        TURRET2
    };

    private TurretType selectedTurret = TurretType.NONE;
    public TurretType WhichTurretSelected => selectedTurret;

    private static Dictionary<TurretType, Object> turrets;

    public MapController map;

    void Awake()
    {
        if (turrets == null)
        {
            turrets = new Dictionary<TurretType, Object>()
            {
                { TurretType.TURRET1, Resources.Load("Prefabs/Turret1") },
                { TurretType.TURRET2, Resources.Load("Prefabs/Turret2") }
            };
        }
    }

    public void ToggleTurretPlacement(TurretType turretType)
    {
        if (selectedTurret != TurretType.NONE)
        {
            selectedTurret = TurretType.NONE;
        }
        else
        {
            selectedTurret = turretType;
        }
    }

    public void PlaceTurret(Vector2 position) //TODO: enum instead ?
    {
        map.PlaceTurret(position, turrets[selectedTurret]);
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
