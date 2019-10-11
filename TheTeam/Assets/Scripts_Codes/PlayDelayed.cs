using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDelayed : MonoBehaviour {
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    float time;

    // Use this for initialization
    void Start () {
        audioSource.PlayDelayed(time);
	}

}
