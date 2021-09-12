using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminInterfaceController : MonoBehaviour
{

    public MapController map;
    public TurretPlacement turretPlacer;



    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlacingTurret()
    {
        turretPlacer.ToggleTurretPlacement();
    }

    public void SendWave()
    {
        WaveEntrypointController entry = map.GetWaveEntrypoints[0];
        entry.EnqueueWave();
    }

    //void
}
