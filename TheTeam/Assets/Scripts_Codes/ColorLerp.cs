using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour {
    public List<Material> materialArray;
    [SerializeField]
    private Material baseMaterial;
    [SerializeField]
    private float time;
    [SerializeField]
    private bool pulseMode = true;
    [SerializeField]
    private bool rainbowMode = false;

    private Renderer rend;
    private int index;
    private float lerp;
    Material target;
    Material nextTarget;

    void Start()
    {
        index = 0;
        target = materialArray[index];
        nextTarget = materialArray[index + 1];
        rend = GetComponent<Renderer>();
        pulseMode = !rainbowMode;
    }

    void Update(){
        //lerp += time * Time.deltaTime;
        lerp = Mathf.PingPong(Time.time, time) / time;
        if (rainbowMode)
            rend.material.Lerp(target, nextTarget, lerp);
        else if (pulseMode)
            rend.material.Lerp(baseMaterial, target, lerp);
        
        if (lerp <= 0.01)
            NextMaterial();
    }

    void NextMaterial()
    {
        Debug.Log("NextMaterial() called.");
        if (index >= (materialArray.Count-1))
            index = 0;
        else
            index++;
        target = materialArray[index];
        if (rainbowMode)
            nextTarget = materialArray[(index+1) % materialArray.Count];
    }
}
