using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonColor : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
       int rand =  Random.Range(0, 4);
        anim.SetInteger("SwitchColor", rand);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
