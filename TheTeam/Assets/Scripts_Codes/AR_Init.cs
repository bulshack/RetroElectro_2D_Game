using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Init : MonoBehaviour {

    //Fields
    [SerializeField]
    private GameObject Splash;
    [SerializeField]
    private GameObject PlaneFinder;
    [SerializeField]
    private GameObject HUD;

    //Debugging //Activate Maze if in active
    //[SerializeField]
    //private GameObject Maze;

    // Use this for initialization
    void Start () {
        Splash.gameObject.SetActive(false);
        HUD.gameObject.SetActive(true);
        PlaneFinder.gameObject.SetActive(false);

        //Debugging //Activate Maze if inactive
        //Maze.gameObject.SetActive(true);
    }
}
