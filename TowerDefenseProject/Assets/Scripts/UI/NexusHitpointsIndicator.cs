using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NexusHitpointsIndicator : MonoBehaviour
{
    private TextMeshProUGUI hitpointsText;
    private int maxHitpoints;
    // Start is called before the first frame update
    
    
    void Awake()
    {
        hitpointsText = transform.Find("Nexus Hitpoints").GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(int maxHitpoints)
    {
        this.maxHitpoints = maxHitpoints;
        hitpointsText.text = "Nexus: " + maxHitpoints + " / " + maxHitpoints;
    }

    public void SetHitpoints(int amount)
    {
        hitpointsText.text = "Nexus: " + amount + " / " + maxHitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
