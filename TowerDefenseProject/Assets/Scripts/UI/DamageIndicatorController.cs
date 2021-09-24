using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicatorController : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshProUGUI text;
    private float shiftSpeed = 50f;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(int damage)
    {
        text.text = damage.ToString();
        StartCoroutine(FadeOutText(timeMultiplier, text));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1, 1, 0) * Time.deltaTime * shiftSpeed;
    }


    //text fading https://stackoverflow.com/questions/56031067/using-coroutines-to-fade-in-out-textmeshpro-text-element
    private int timeMultiplier = 1;

    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
        Destroy(gameObject);
    }

    public void FadeInText(float timeSpeed = -1.0f)
    {
        if (timeSpeed <= 0.0f)
        {
            timeSpeed = timeMultiplier;
        }
        StartCoroutine(FadeInText(timeSpeed, text));
    }
    public void FadeOutText(float timeSpeed = -1.0f)
    {
        if (timeSpeed <= 0.0f)
        {
            timeSpeed = timeMultiplier;
        }
        StartCoroutine(FadeOutText(timeSpeed, text));
    }

}
