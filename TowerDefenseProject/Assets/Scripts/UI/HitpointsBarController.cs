using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsBarController : MonoBehaviour
{
    private Slider slider;
    private Image background;
    private Image fillArea;

    private Transform entity;

    private static Object damageIndicator;

    private int currentHitPoints;
    private int maxHitpoints;
    private bool initialized = false;
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(float value)
    {
        fillArea.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        slider.value = (value / maxHitpoints);
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

    public void TakeDamage(int amount)
    {
        currentHitPoints -= amount;
        SetHealthBarValue(currentHitPoints);
        GameObject initialized = (GameObject)Instantiate(damageIndicator, transform.position + new Vector3(0, 10, 0), Quaternion.identity, transform);
        DamageIndicatorController indicator = initialized.GetComponent<DamageIndicatorController>();
        indicator.Initialize(amount);
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
        background = transform.Find("Background").GetComponent<Image>();
        if (damageIndicator == null)
        {
            damageIndicator = Resources.Load("Prefabs/DamageIndicator");
        }
    }

    public void Initialize(EnemyController enemy)
    {
        this.entity = enemy.transform;
        this.maxHitpoints = enemy.GetHitpoints;
        this.currentHitPoints = this.maxHitpoints;
        SetHealthBarValue(maxHitpoints);
        fillArea.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
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
