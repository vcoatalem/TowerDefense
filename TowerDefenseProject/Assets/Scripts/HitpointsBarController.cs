using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsBarController : MonoBehaviour
{
    private Image healthBarImage;
    private Image healthBarBackground;

    private Transform entity;

    private int maxHitpoints;
    private bool initialized = false;
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(float value)
    {
        healthBarImage.fillAmount = value;
        if (healthBarImage.fillAmount < (maxHitpoints * 30 / 100))
        {
            SetHealthBarColor(Color.red);
        }
        else if (healthBarImage.fillAmount < (maxHitpoints * 50 / 100))
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
        return healthBarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public void SetHealthBarColor(Color healthColor)
    {
        healthBarImage.color = healthColor;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }

    public void Initialize(Transform enemy)
    {
        this.entity = enemy;
        initialized = true;
    }

    private void Update()
    {
        if (entity)
        {
            transform.position = Camera.main.WorldToScreenPoint(entity.position);
        }
        if (!entity && initialized)
        {
            Destroy(gameObject);
        }
    }
}
