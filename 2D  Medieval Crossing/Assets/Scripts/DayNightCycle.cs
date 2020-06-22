using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    [Header("Debug")]

    [SerializeField] string currentHour;

    [Header("Time Settings")]

    public float currentTime;
    public float timeConversion;
    public int secondsPerDay = 1200;


    [Header("Light Settings")]

    [SerializeField] Light2D light2D;
    [SerializeField] float currentIntensity;
    [SerializeField] Gradient gradient;
    [SerializeField] AnimationCurve curve;


    // Start is called before the first frame update
    void Start()
    {
        //86400 s in one day
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= secondsPerDay) currentTime = 0;
        currentTime += Time.deltaTime;
        UpdateLight();
        ConvertSecondsToInGameHours();
    }

    private void OnValidate()
    {
        if (currentTime > secondsPerDay) currentTime = 0;
        if (currentTime < 0) currentTime = secondsPerDay-0.1f;
        ConvertSecondsToInGameHours();
        UpdateLight();
    }

    void ConvertSecondsToInGameHours()
    {
        System.TimeSpan result = System.TimeSpan.FromSeconds(currentTime*86400/ secondsPerDay);
        System.DateTime actualResult = System.DateTime.MinValue.Add(result);
        currentHour = actualResult.ToString("HH:mm");
    }

    void UpdateLight()
    {
        light2D.color = gradient.Evaluate(currentTime / secondsPerDay);
        //light2D.intensity = 0.5f + 0.5f * Mathf.Sin(currentTime / secondsPerDay);
        light2D.intensity = curve.Evaluate(currentTime / secondsPerDay);
        currentIntensity = light2D.intensity;

    }
}
