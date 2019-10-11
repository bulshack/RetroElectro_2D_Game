using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public Transform panel;

   private GameObject BgM;
    
    

  //public GameObject BackgroundMusic = GameObject.FindGameObjectsWithTag("Music");;
 
   private bool pause;

     void start()
    {
        pause = false;

       panel = GameObject.Find("Pause_Panel").transform;

        BgM = GameObject.Find("BackGroundMusic");


        if (panel == null)
        {
            Debug.Log("The Pause Panel can not be found in the scene");
            this.enabled = false; 
            return;
        }

      //  panel.gameObject.SetActive(true);
    }


    // Use this for initialization
  

    // Update is called once per frame
    void Update()
    {

        if (panel != null)
        {


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = false;
                //Debug.Log("P!");
            }

            if (pause)
            {
                Time.timeScale = 0;
                panel.gameObject.SetActive(true); 

                if (BgM != null)
                {
                    BgM.GetComponent<AudioSource>().Pause();
                }

                //Debug.Log("Paused!");
            }

            else if (!pause)
            {
                Time.timeScale = 1;

                panel.gameObject.SetActive(false);

                if (BgM != null)
                {
                    BgM.GetComponent<AudioSource>().UnPause();
                }

                // Debug.Log("Unpaused!");
            }
        }
    } 


    public void Pause_B()
    {

        pause = true;
    }
}
