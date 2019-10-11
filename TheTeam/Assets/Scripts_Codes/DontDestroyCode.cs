using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCode : MonoBehaviour {

    void Awake()
    {
        GameObject[] MusicObject = GameObject.FindGameObjectsWithTag("Music");
        if (MusicObject.Length > 1)        
        {
            Destroy(this.gameObject);
        } 

        DontDestroyOnLoad(this.gameObject);
    }
}
