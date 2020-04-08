using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunset : MonoBehaviour
{
    public bool lockX;

    public float startIntensity;
    public float endIntensity;
    public float duration;

    private float currentTime;
    public Light lt;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        var progress = Math.Min(1.0f, currentTime/duration);

        Debug.Log("progress = " + progress);

        if (endIntensity > startIntensity)  {
            var delta = endIntensity - startIntensity;
            lt.intensity = startIntensity + (progress * delta);
        } else {
            var delta = startIntensity - endIntensity;
            lt.intensity = startIntensity - (progress * delta);
        }
    }
}
