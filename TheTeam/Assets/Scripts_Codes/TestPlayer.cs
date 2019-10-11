using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

   public GameObject[] Burners = new GameObject[4];
    int direction;
    Animator anim;
    Rigidbody2D rb;
    public float X, Y;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
       
        rb = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") >0)
        {
            direction = 2;
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            direction = 4;
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 5, 0);
        }
        else if (Input.GetAxis("Vertical")>0)
        {
            direction = 1;
            rb.velocity = new Vector2(0, Input.GetAxis("Vertical") * 5);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            direction = 3;
            rb.velocity = new Vector2(0, Input.GetAxis("Vertical") * 5);
        }
        switch (direction)
        {
            case 3:
                Burners[0].GetComponent<Animator>().SetBool("On", true);
                Burners[1].GetComponent<Animator>().SetBool("On", false);
                Burners[2].GetComponent<Animator>().SetBool("On", false);
                Burners[3].GetComponent<Animator>().SetBool("On", false);
               
                break;
            case 4:
                Burners[0].GetComponent<Animator>().SetBool("On", false);
                Burners[1].GetComponent<Animator>().SetBool("On", true);
                Burners[2].GetComponent<Animator>().SetBool("On", false);
                Burners[3].GetComponent<Animator>().SetBool("On", false);
              
                break;
            case 1:
                Burners[1].GetComponent<Animator>().SetBool("On", false);
                Burners[0].GetComponent<Animator>().SetBool("On", false);
                Burners[2].GetComponent<Animator>().SetBool("On", true);
                Burners[3].GetComponent<Animator>().SetBool("On", false);
            
                break;
            case 2:
                Burners[0].GetComponent<Animator>().SetBool("On", false);
                Burners[1].GetComponent<Animator>().SetBool("On", false);
                Burners[2].GetComponent<Animator>().SetBool("On", false);
                Burners[3].GetComponent<Animator>().SetBool("On", true);
                break;
               
            default:
                break;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < Burners.Length; i++)
            {
                Burners[i].GetComponent<Animator>().SetBool("On",true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < Burners.Length; i++)
            {
                Burners[i].GetComponent<Animator>().SetBool("On", false);
            }
        }
	}
}
