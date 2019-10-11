using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.Play("GroundTIle",0,Random.value);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
