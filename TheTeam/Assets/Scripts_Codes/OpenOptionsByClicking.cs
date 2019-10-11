using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenOptionsByClicking : MonoBehaviour, IPointerClickHandler
{

    public bool active;
	// Use this for initialization
	void Start () {
		
	}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.clickCount == 2)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            active = true;
            //GetComponentInChildren<GameObject>().SetActive(true);
        }
        else if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            active = false;
        }
    }
    }
