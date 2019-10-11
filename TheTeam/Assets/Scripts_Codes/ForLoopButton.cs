using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ForLoopButton : MonoBehaviour
{
    public GameObject slot;
    public InputField input;
    public GameObject eventToIterate;
    public int Iterartions;

    // Use this for initialization
    void Start()
    {

    }
    public void OnClick()
    {
        string tagname;
        eventToIterate = slot.GetComponent<Slots>().obj;
        tagname = eventToIterate.tag;
        switch (tagname)
        {
            case "Move_Tool":
                for (int i = 0; i < Iterartions; i++)
                {
                    EventManager.C_Move();
                }
                break;
            case "Jump_Tool":
                for (int i = 0; i < Iterartions; i++)
                {
                    EventManager.C_Jump();
                }
                break;
            case "Minus90_Tool":
                for (int i = 0; i < Iterartions; i++)
                {
                    EventManager.C_RotateClockWise();
                }
                break;
            case "Plus90_Tool":
                for (int i = 0; i < Iterartions; i++)
                {
                    EventManager.C_RotateCounterClockWise();
                }
                break;

            default:
                break;
        }
    }
    public void StoreNumber()
    {
        Iterartions = Convert.ToInt32(input.text);

    }
    // Update is called once per frame
    void Update()
    {

    }
}

