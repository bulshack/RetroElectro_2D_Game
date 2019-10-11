using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Button : MonoBehaviour
{


    public GameObject slotss;

    public GameObject[] ArraySlots = new GameObject[28];

    public GameObject ExecuteButton;


    private void Start()
    {

        ExecuteButton = GameObject.Find("Execute_Button");


        if (ExecuteButton == null)
        {
            Debug.Log("No Execute Button on the scene, check  Line 22");
            return;
        } 



        for (int i = 0; i < ArraySlots.Length; i++)
        {
            ArraySlots[i] = slotss.transform.GetChild(i).gameObject;
        }

    }


    
    public void OnClick()
    {

        if (ExecuteButton.tag == "Executing")
        {
            return;
        }


        EventManager.C_Reset();
        for (int i = 0; i < ArraySlots.Length; i++)
        {
            if (ArraySlots[i].transform.childCount > 1)
            {
                Destroy(ArraySlots[i].transform.GetChild(1).gameObject);
            }

        }

    }
}
