using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour {



    Animator _CurrentAnimation;

    // Use this for initialization
    void Start () {



        _CurrentAnimation = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();


        if (_CurrentAnimation == null)
        {
            Debug.Log("_CurrentAnimation Variable is Null Line 20, SortByClicking");
            return;
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("I am On the mouse");
    }

    private void OnMouseExit()
    {

        Debug.Log("No Mouse");
    }
}
