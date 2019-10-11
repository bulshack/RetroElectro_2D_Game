using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleScript : MonoBehaviour {


    Animator anim;
   public float timer;
    public int portalNumber;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        timer = 7;
        anim.SetBool("Purple",true);
        portalNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

       

        if (timer < 0)
        {
            
            switch (portalNumber)
            {
                case 0:
                    anim.SetBool("Blue",true);
                    anim.SetBool("Green", false);
                    anim.SetBool("Purple", false);
                    break;
                case 1:
                    anim.SetBool("Green", true);
                    anim.SetBool("Purple", false);
                    anim.SetBool("Blue", false);
                    break;
                case 2:
                    anim.SetBool("Purple", true);
                    anim.SetBool("Blue", false);
                    anim.SetBool("Green", false);
                    break;
                default:
                    break;
            }
            ++portalNumber;
            if (portalNumber > 2)
                portalNumber = 0;
         

            timer = 7;
        }

	}
}
