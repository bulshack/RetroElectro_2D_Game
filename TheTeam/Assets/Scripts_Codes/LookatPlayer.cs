using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatPlayer : MonoBehaviour
{

    public Transform Target;
    public bool FacingRight;
   public float X, tX;
    // Use this for initialization
    void Start()
    {
       
         FacingRight = false;
}

    // Update is called once per frame
    void Update()
    {
        X = transform.position.x;
        tX = Target.position.x;
        if (FacingRight) { //GetComponent<SpriteRenderer>().flipX = false;
        }
        else             { //GetComponent<SpriteRenderer>().flipX = true;
        }

        if (X < tX)
            FacingRight = true;
        else
            FacingRight = false;
            
      
    }
}
