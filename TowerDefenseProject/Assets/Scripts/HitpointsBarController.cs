using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsBarController : MonoBehaviour
{
    private Slider slider;
    private Image fillArea;

    private Transform entity;

    private int maxHitpoints;
    private bool initialized = false;
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(float value)
    {
        slider.value = (value / maxHitpoints);
        Debug.Log("slider value: " + slider.value);
        if (slider.value < 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (slider.value < 0.5f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public float GetHealthBarValue()
    {
        return slider.value;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public void SetHealthBarColor(Color healthColor)
    {
        fillArea.color = healthColor;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 1;
        fillArea = transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
    }

    public void Initialize(EnemyController enemy)
    {
        this.entity = enemy.transform;
        this.maxHitpoints = enemy.GetHitpoints;
        SetHealthBarValue(maxHitpoints);
        initialized = true;
    }

    private void Update()
    {
        if (entity)
        {
            Vector3 enemyPosition = Camera.main.WorldToScreenPoint(entity.position);
            transform.position = new Vector3(enemyPosition.x, enemyPosition.y + 12, enemyPosition.z); //TODO: tweak y parameter
        }
        if (!entity && initialized)
        {
            Destroy(gameObject);
        }
    }
}
