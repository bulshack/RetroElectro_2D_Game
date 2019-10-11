using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Threading;

public class colorMutator : MonoBehaviour {

    public Color color = Color.magenta;
    public static Stopwatch timer;
    //public float timerElapsed;

    void Start()
    {
        //timerElapsed = 0.0;
        timer = new Stopwatch();
        timer.Start();
        //Renderer renderer = GetComponent<Renderer>();
        //renderer.material.color = color;
        //Image image = GetComponent<Image>();
    }

    void Update()
    {
        color.g += (float)(timer.ElapsedMilliseconds % 256);
        color.b += (float)(timer.ElapsedMilliseconds % 256);
        color.r += (float)(timer.ElapsedMilliseconds % 256);
    }

    private void OnDestroy()
    {
        timer.Stop();
    }
}
