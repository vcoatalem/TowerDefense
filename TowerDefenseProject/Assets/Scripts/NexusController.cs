using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour
{
    private int hitpoints;
    private Vector2 gridPosition;
    public Vector2 GetGridPosition => gridPosition;

    private NexusHitpointsIndicator hitpointsIndicator;

    private static Object NexusHitpointsIndicatorPrefab;
    private GameObject gameStatusPanel;

    // Start is called before the first frame update
    void Awake()
    {
        if (!NexusHitpointsIndicatorPrefab)
        {
            NexusHitpointsIndicatorPrefab = Resources.Load("Prefabs/NexusHitpointsIndicator");
        }
        gameStatusPanel = GameObject.Find("Game Status Panel");
        if (!gameStatusPanel)
        {
            Debug.LogError("Could not load 'Game Status Panel'");
        }

        hitpoints = 200;
        hitpointsIndicator = ((GameObject)Instantiate(NexusHitpointsIndicatorPrefab, Vector3.zero, Quaternion.identity, gameStatusPanel.transform)).GetComponent<NexusHitpointsIndicator>();
        hitpointsIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600, 10, 0);
        hitpointsIndicator.Initialize(hitpoints);

        gridPosition = new Vector2(transform.position.x, transform.position.z); //TODO: for now we will do this assumption
    }


    public void TakeHit(int damage)
    {
        hitpoints -= damage;
        hitpointsIndicator.SetHitpoints(hitpoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
