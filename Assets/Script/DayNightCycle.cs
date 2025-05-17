using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [Range(0f, 1f)] 
    public float time;
    public float fullDayLengh;
    public float startTime = 0.4f;
    private float _timeRate;
    public Vector3 noon; // Vector 90 0 0
    
    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;
    
    [Header("Other lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        _timeRate = 1.0f / fullDayLengh;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + _timeRate * Time.deltaTime) % 1f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f; // 괜히 불편해.. 이거 심지어 강의 내용 그대론데..
        // 정오 계산
        lightSource.color = gradient.Evaluate(time);
        lightSource.intensity = intensity;
        GameObject go = lightSource.gameObject;

        if (lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}








