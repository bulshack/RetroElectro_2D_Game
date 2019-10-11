using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExecuteScript_rev2 : MonoBehaviour
{
    public GameObject slotss;
    public GameObject[] ArraySlots = new GameObject[28];
    public Scene SpeedFlag;
    public GameObject ExecuteButton;
    public GameObject ToolPanel; 
    public Sprite oldImg;  
    public Sprite Move;
    public Sprite Jump;
    public Sprite Rotate90;
    public Sprite RotateMinus90;
    string Tag;
 

    private void Start()
    {    
        gameObject.tag = "ReadyToExecute";

        SpeedFlag = SceneManager.GetActiveScene();

        slotss = GameObject.Find("Slots");

  

        if (slotss == null)
        {
            Debug.Log("The Game Object Slots does not exist on the scene or it is disable");

            this.enabled = false;

            Debug.Log(this.name + " have been disable to avoid unstable behavior");

            return;
        }


        for (int i = 0; i < ArraySlots.Length; i++)
        {
            ArraySlots[i] = slotss.transform.GetChild(i).gameObject;
        }


        ToolPanel = GameObject.Find("Tool_Panel");


        if (ToolPanel == null)
        {
            Debug.Log(" Game Object Tool Box Missing, Check Line 55 in the fucntion Start");

            return;

        }



        //////////////////// Loading Sprites ////////////////////////////////////////
        Move = ToolPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite;
        Jump =ToolPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite;
        Rotate90 = ToolPanel.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite;
        RotateMinus90 = ToolPanel.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite;
        ////////////////////////////////////////////////////////////////////////////////////
    }


    #region Event Methods 

    public void OnClick()
    {       
         if (this.tag == "ReadyToExecute" && checkElementsInArray() == true)
         {

            gameObject.tag = "Executing";

            this.transform.GetChild(0).GetComponent<Text>().text = "STOP";

            if (checkElementsInArray())
            {
                RayCastPrevention();

                if (SpeedFlag.name[0] == 'L')
                {
                    EventManager.C_Begin();
                }

                uint seconds = 1; 

                Sprite oldImg; 

                for (int i = 0; i < ArraySlots.Length; ++i)
                {
                    if (ArraySlots[i].transform.childCount > 1)
                    {
                        oldImg = null;


                        switch (ArraySlots[i].transform.GetChild(1).tag)
                        {
                            case "Jump_Tool":

                                oldImg = Jump; 

                                break;

                            case "Move_Tool":

                                oldImg = Move; 

                                break;

                            case "Plus90_Tool": 

                                oldImg = Rotate90; 

                                break;

                            case "Minus90_Tool":

                                oldImg = RotateMinus90; 

                                break;
                        }



                        StartCoroutine(ChangeToolColor(ArraySlots[i].transform.GetChild(1).transform.GetChild(0).gameObject, seconds, Color.green, oldImg));  


                        StartCoroutine(ExecuteTool(ArraySlots[i].transform.GetChild(1).tag, seconds, ArraySlots[i]));


                        if (SpeedFlag.name[0] == 'L')
                        {
                            seconds += 3;


                        }

                        else
                        {
                            seconds += 1;

                        }


                        StartCoroutine(ChangeToolColor(ArraySlots[i].transform.GetChild(1).transform.GetChild(0).gameObject, seconds, Color.cyan, oldImg));
                    }
                }

                StartCoroutine(I_Reset(++seconds));

            

            }

           
        }

        else if (this.tag == "Executing")
        {
            STOP();

            this.transform.GetChild(0).GetComponent<Text>().text = "EXECUTE";
        }

       
    }

    #endregion
  
    #region Functionality Methods
     public IEnumerator ExecuteTool(string _type, uint _time, GameObject _tool)
    {
        yield return new WaitForSeconds(_time);

       
        //TODO: Make the tool game object passed in highlight when it calls the event 



        Debug.Log("Tool executed: " + _tool.name); 
        Debug.Log(_type);

      //  _tool.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Image>().color = Color.green;

        switch (_type)
        {
            case "Jump_Tool":
                
                EventManager.C_Jump();
                break;

            case "Move_Tool":
                EventManager.C_Move();
                break;

            case "Plus90_Tool":
                EventManager.C_RotateCounterClockWise();
                break;

            case "Minus90_Tool":
                EventManager.C_RotateClockWise();
                break;
        }


    //   _tool.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Image>().color = Color.cyan;

    }

    IEnumerator I_Reset(uint _time)
    {
       
        yield return new WaitForSeconds(_time);
        EventManager.C_Reset();     
        this.transform.GetChild(0).GetComponent<Text>().text = "EXECUTE";
        this.tag = "ReadyToExecute";
        RayCastPrevention();
    }

    public void Reset()
    {
        EventManager.C_Reset();
        int index = 0;
        for (int i = 0; i < ArraySlots.Length; i++)
        {
            for (int j = 0; j < ArraySlots[i].transform.childCount; j++)
            {
                index = j;
                if (index == 1)
                {
                    Transform temp = ArraySlots[i].transform.GetChild(1);
                    if (temp == null)
                    {
                        return;
                    }
                    Destroy(temp.gameObject);
                }
            }
        }

        StopAllCoroutines();        
    } 
     

    IEnumerator ChangeToolColor(GameObject _tool, uint time,Color color, Sprite oldSprite)
    {
        yield return new WaitForSeconds(time);

        if (color == Color.green)
        {
            _tool.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            _tool.transform.GetComponent<Image>().color = color;
           

        }

        else if(color == Color.cyan)
        {
            _tool.transform.GetComponent<Image>().color = color;
            _tool.transform.GetChild(0).GetComponent<Animator>().enabled = false;
            _tool.transform.GetChild(0).GetComponent<Image>().sprite = oldSprite;

        }

    }
        


    public void STOP()
    {

        
        EventManager.C_Reset();
        

        for (int i = 0; i < ArraySlots.Length; i++)
        {
            if (ArraySlots[i].transform.childCount > 1)
            {


                switch (ArraySlots[i].transform.GetChild(1).tag)
                {
                    case "Jump_Tool":

                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().enabled = false;  

                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = Jump;

                        break;

                    case "Move_Tool":

                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().enabled = false;
                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = Move;

                        break;

                    case "Plus90_Tool": 

                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().enabled = false;
                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = Rotate90;

                        break;

                    case "Minus90_Tool": 

                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().enabled = false;
                        ArraySlots[i].transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = RotateMinus90;

                        break;
                }


                ArraySlots[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.cyan;
            }
        }

        this.tag = "ReadyToExecute"; 

        RayCastPrevention();

        
        StopAllCoroutines();

        this.transform.GetChild(0).GetComponent<Text>().text = "EXECUTE";

        this.tag = "ReadyToExecute";
    } 


  
    bool checkElementsInArray()
    {
        for (int i = 0; i < ArraySlots.Length; i++)
        {
            if (ArraySlots[i].transform.childCount > 1)
            {
                return true;
            } 
        }

        return false;
    }



    void RayCastPrevention()
    { 

       if (checkElementsInArray())
        {
            for (int i = 0; i < ArraySlots.Length; i++)
            {
                if (ArraySlots[i].transform.childCount > 1)
                {

                    if (this.tag == "Executing")
                    {
                        ArraySlots[i].transform.GetChild(1).transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }

                    else if (this.tag == "ReadyToExecute")
                    {
                        ArraySlots[i].transform.GetChild(1).transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    
                }
            }
        }
    }
    #endregion
}






