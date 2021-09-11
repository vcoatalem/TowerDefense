using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour
{
    private int hitpoints;
    private Vector2 gridPosition;
    public Vector2 GetGridPosition => gridPosition;

    // Start is called before the first frame update
    void Awake()
    {
        gridPosition = new Vector2(transform.position.x, transform.position.z); //TODO: for now we will do this assumption
        hitpoints = 50;
    }


    public void TakeHit(int damage)
    {
        hitpoints -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
