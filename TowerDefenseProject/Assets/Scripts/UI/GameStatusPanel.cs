using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatusPanel : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshProUGUI goldIndicator;
    //private TextMeshProUGUI nexusIndicator;
    void Awake()
    {
        goldIndicator = transform.Find("Player Gold").GetComponent<TextMeshProUGUI>();
        //nexusIndicator = transform.Find("Nexus Hitpoints").GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(int hitpoints, int gold)
    {
        //SetNexusHitpoints(hitpoints);
        SetGold(gold);
    }

/*    public void SetNexusHitpoints(int amount)
    {
        nexusIndicator.text = "Nexus Hitpoints: " + amount.ToString();
    }
*/
    public void SetGold(int amount)
    {
        goldIndicator.text = "Gold: " + amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
