using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SortByClicking : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject slotss;

    private GameObject[] _Slots = new GameObject[28];

  public  Animator _CurrentAnimation;

    public Sprite OldImg;


    private void Start()
    {

      


        _CurrentAnimation = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();



        if (_CurrentAnimation == null)
        {
            Debug.Log("_CurrentAnimation Variable is Null Line 20, SortByClicking");
            return;
        }


        _CurrentAnimation.enabled = false;


        OldImg = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite;

        if (OldImg == null)
        {
            Debug.Log("OldImg is null Check line 39");
            return;
        }

        slotss = GameObject.Find("Slots");


        if (slotss == null)
        {
            Debug.Log("The Game Object Slots does not exist on the scene or it is disable");

            this.enabled = false;

            Debug.Log(this.name + " have been disable to avoid unstable behavior");

            return;
        }


        for (int i = 0; i < _Slots.Length; i++)
        {
            _Slots[i] = slotss.transform.GetChild(i).gameObject;
        }
    }
   


    public void OnPointerClick(PointerEventData pointerEventData)
    {
       
        if (pointerEventData.clickCount >= 1)     

        {
            
            if (gameObject.transform.parent.name[0] != 'S')
            {
                for (int i = 0; i < _Slots.Length; i++)
                {
                    if (_Slots[i].transform.childCount == 1)
                    {

                        var Clone = Instantiate(gameObject, _Slots[i].transform, true);

                        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                        audioSource.Play();

                        Clone.transform.SetParent(_Slots[i].transform);

                        Clone.transform.position = _Slots[i].transform.position;

                        Clone.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = OldImg;

                        // Destroy(Clone.GetComponent("SortByClicking"));
                        break;

                    }
                }

            }
        }

         if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (gameObject.transform.parent.parent.name == _Slots[0].transform.parent.name)
            {
                Destroy(gameObject);
            }   

        }

    }


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message 
        _CurrentAnimation.enabled = true;
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _CurrentAnimation.enabled = false;

        this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = OldImg;

        Debug.Log("Cursor Exiting " + name + " GameObject");
    }



}
        

  