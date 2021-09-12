using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private TurretPlacement turretPlacement;
    void Awake()
    {
        turretPlacement = FindObjectOfType<TurretPlacement>();
    }


    public void OnMouseOver()
    {
        this.transform.localScale = new Vector3(1, 0.3f, 1);
        if (Input.GetMouseButtonDown(0) && turretPlacement.IsPlacingTurret)
        {
            turretPlacement.PlaceTurret(new Vector2(this.transform.position.x, this.transform.position.z));
        }
    }

    public void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1, 0.1f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
