using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    private TurretTemplate template;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        template.Behave(transform.position); //TODO: should this be called every frame ? (prob not)
    }

    public void Initialize(TurretTemplate template)
    {
        this.template = template;
    }

    public Vector2 GetGridPosition()
    {
        return new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.z));
    }
}
